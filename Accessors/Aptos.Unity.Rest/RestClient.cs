using Aptos.Accounts;
using Aptos.BCS;
using Aptos.Unity.Rest.Model;
using Chaos.NaCl;
using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Transaction = Aptos.Unity.Rest.Model.Transaction;
using Org.BouncyCastle.Asn1.Ocsp;
using DemoUI.Models;
using System.Threading.Tasks;

namespace Aptos.Unity.Rest
{
    public static class ClientConfig
    {
        public const int EXPIRATION_TTL = 600;
        public const int GAS_UNIT_PRICE = 100;
        public const int MAX_GAS_AMOUNT = 100000;
        public const int TRANSACTION_WAIT_IN_SECONDS = 20; // Amount of seconds to each during each polling cycle.
    }

    /// <summary>
    /// The Aptos REST Client contains a set of [standalone] Coroutines
    /// that can started within any Unity script.
    ///
    /// Consideration must be placed into the wait time required
    /// for a transaction to be committed into the blockchain.
    /// </summary>
    public class RestClient
    {
        /// Static instance of the REST client.
        public static RestClient Instance { get; private set; }

        /// Based enpoint for REST API.
        public Uri Endpoint { get; set; }
        public int? ChainId { get; set; }
        private static readonly HttpClient httpClient = new HttpClient();

        #region Transactions

        /// <summary>
        /// Submits a BCS transaction.
        /// </summary>
        /// <param name="callback">Callback function used after response is received with the JSON response.</param>
        /// <param name="SignedTransaction">The signed transaction.</param>
        /// <returns></returns>
        public void SubmitBCSTransaction(
            Action<string, ResponseInfo> callback,
            SignedTransaction SignedTransaction
        )
        {
            string submitTxnEndpoint = Endpoint + "/transactions";

            HttpContent content = new ByteArrayContent(SignedTransaction.Bytes());
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x.aptos.signed_transaction+bcs");
            var apiRes = httpClient.PostAsync(submitTxnEndpoint, content);
            apiRes.Wait();
            var apiData = apiRes.Result;

            ResponseInfo responseInfo = new ResponseInfo();
            if (apiData != null && apiData.IsSuccessStatusCode)
            {
                var response = apiData.Content.ReadAsStringAsync();
                response.Wait();

                responseInfo.status = ResponseInfo.Status.Success;
                responseInfo.message = response.Result.ToString();
                callback(response.Result.ToString(), responseInfo);
            }
            else
            {
                responseInfo.status = ResponseInfo.Status.Failed;
                responseInfo.message = "Error while submitting BCS transaction. " + apiData.RequestMessage.ToString();
                callback(null, responseInfo);
            }
        }

        public async Task<bool> WaitForTransaction(string txnHash)
        {
            int count = 0;  // Current attempt at querying for hash
            bool isTxnPending = true; // Has the transaction been confirmed in the blockchain

            bool isTxnSuccessful = false; // Was the transaction successful
            ResponseInfo responseInfo = new ResponseInfo();
            responseInfo.status = ResponseInfo.Status.Failed;

            while (isTxnPending)
            {
                ResponseInfo responseInfoTxnPending = new ResponseInfo();

                // Check if the transaction hash can be found
                isTxnPending = await TransactionPending(txnHash);

                // If transaction hash has been found in the blockchain (not "pending"), check if it was succesful
                if (!isTxnPending)
                {
                    break;
                }

                // Timeout if the transaction is still pending (hash not found) and we have queried N times
                // Set the boolean response to false, break -- we did not find the transaction
                if (count > ClientConfig.TRANSACTION_WAIT_IN_SECONDS)
                {
                    responseInfo.message = "Response Timed Out After Querying " + count + "Times";
                    isTxnSuccessful = false;
                    break;
                }

                count += 1;
                await Task.Delay(2000);
            }
            return isTxnPending;
        }

        /// <summary>
        /// Query to see if transaction has been 'confirmed' in the blockchain by using the transaction hash.
        /// A 404 error will be returned if the transaction hasn't been confirmed.
        /// Once the transaction is confirmed it will have a `pending_transaction` state.
        /// </summary>
        /// <param name="callback">Callback function used when response is received.</param>
        /// <param name="txnHash">Transaction hash being queried for.</param>
        /// <returns>Calls <c>callback</c>function with <c>(bool, ResponseInfo)</c>: \n
        /// A boolean that is: \n
        /// -- true if transaction is still pending / hasn't been found, meaning 404, error in response, or `pending_transaction` is true \n
        /// -- false if transaction has been found, meaning `pending_transaction` is true \n
        ///
        /// A response object that contains the response details.
        /// </returns>
        public async Task<bool> TransactionPending(string txnHash)
        {
            string txnURL = Endpoint + "/transactions/by_hash/" + txnHash;
            Uri txnURI = new Uri(txnURL);
            var apiData = await httpClient.GetAsync(txnURI);

            if (apiData != null && apiData.IsSuccessStatusCode)
            {
                Transaction trans = JsonConvert.DeserializeObject<Transaction>(await apiData.Content.ReadAsStringAsync(), new TransactionConverter());

                bool isPending = trans.Type.Equals("pending_transaction");
                return isPending;
            }
            return true;
        }

        public void CreateBCSTransaction(
            Action<RawTransaction> Callback,
            Account Sender,
            BCS.TransactionPayload payload)
        {
            string sequenceNumber = "";
            ResponseInfo responseInfo = new ResponseInfo();

            GetAccountSequenceNumber((_sequenceNumber, _responseInfo) => {
                sequenceNumber = _sequenceNumber;
                responseInfo = _responseInfo;
                if (responseInfo.status != ResponseInfo.Status.Success)
                {
                    throw new Exception("Unable to get sequence number for: " + Sender.AccountAddress + ".\n" + responseInfo.message);
                }

                ulong expirationTimestamp = ((ulong)(DateTime.Now.ToUnixTimestamp() + Constants.EXPIRATION_TTL));

                RawTransaction rawTxn = new RawTransaction(
                    Sender.AccountAddress,
                    int.Parse(sequenceNumber),
                    payload,
                    ClientConfig.MAX_GAS_AMOUNT,
                    ClientConfig.GAS_UNIT_PRICE,
                    expirationTimestamp,
                    (int)this.ChainId
                );

                Callback(rawTxn);
            }, Sender.AccountAddress);


        }

        public void CreateBCSSignedTransaction(
            Action<SignedTransaction> Callback,
            Account Sender,
            BCS.TransactionPayload Payload
        )
        {

            CreateBCSTransaction((rawTransaction) => {
                Accounts.Signature signature = Sender.Sign(rawTransaction.Keyed());
                Authenticator authenticator = new Authenticator(
                    new Ed25519Authenticator(Sender.PublicKey, signature)
                );

                Callback(new SignedTransaction(rawTransaction, authenticator));
            }, Sender, Payload);

            
        }

        public void GetAccountSequenceNumber(Action<string, ResponseInfo> callback, AccountAddress accountAddress)
        {
            GetAccount((_accountData, _responseInfo) => {
                if (_responseInfo.status != ResponseInfo.Status.Success)
                {
                    callback(null, _responseInfo);
                }
                string sequenceNumber = _accountData.SequenceNumber;
                callback(sequenceNumber, _responseInfo);
            }, accountAddress);
        }

        public void GetAccount(Action<AccountData, ResponseInfo> callback, AccountAddress accountAddress)
        {
            string accountsURL = Endpoint + "/accounts/" + accountAddress.ToString();
            Uri accountsURI = new Uri(accountsURL);
            var apiRes = httpClient.GetAsync(accountsURI);
            apiRes.Wait();
            var apiData = apiRes.Result;

            ResponseInfo responseInfo = new ResponseInfo();
            if (apiData != null && apiData.IsSuccessStatusCode)
            {
                responseInfo.status = ResponseInfo.Status.Success;
                responseInfo.message = apiData.RequestMessage.ToString();
                var processedData = apiData.Content.ReadAsAsync<AccountData>();
                processedData.Wait();
                callback(processedData.Result, responseInfo);
            }
            else
            {
                responseInfo.status = ResponseInfo.Status.Failed;
                responseInfo.message = apiData.RequestMessage.ToString();

                callback(null, responseInfo);
            }
        }

        #endregion
        #region Utilities
        /// <summary>
        /// Utility Function coverts byte array to hex
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        private string ToHex(byte[] seed)
        {
            var hex = BitConverter
                .ToString(seed)
                .Replace("-", "")
                .ToLower();

            return hex;
        }

        /// <summary>
        /// Convert byte array to string.
        /// </summary>
        /// <param name="hex">Hexadecimal string</param>
        /// <returns>Byte array representing the hex string.</returns>
        public byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Turns byte array to hexadecimal string.
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>String that represents byte array of hexadecials.</returns>
        public string ToHexadecimalRepresentation(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length << 1);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }
        #endregion
    }
}
