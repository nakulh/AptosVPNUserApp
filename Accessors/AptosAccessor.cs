using Aptos.Accounts;
using Aptos.BCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Aptos.Unity.Rest;
using Aptos.Unity.Rest.Model;
using Newtonsoft.Json;
using Transaction = Aptos.Unity.Rest.Model.Transaction;
using System.Security.Cryptography.X509Certificates;
using static NBitcoin.Scripting.OutputDescriptor;
using Mirage.Aptos.SDK;
using Account = Aptos.Accounts.Account;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace DemoUI.Accessors
{
    internal class AptosAccessor
    {
        private static readonly string vpnPackageId = "0x208299ef9b9bf55204ffd79afa135b2662e14b0e0f843385e4ecc69d0f48bd30";
        private static string NodeUrl = "https://api.testnet.aptoslabs.com/v1";
        private static string FaucetUrl = "https://faucet.testnet.aptoslabs.com";
        private static RestClient aptosRestClient = new RestClient();
        public static async Task<string> purchaseVPN(string vpnProviderObjectId, string privateKeyStr)
        {
            aptosRestClient.Endpoint = new Uri(NodeUrl);
            aptosRestClient.ChainId = 2; //testnet
            PrivateKey privatekey = new PrivateKey(privateKeyStr);
            var keybytes = privatekey.KeyBytes;
            Aptos.Accounts.PublicKey publicKey = privatekey.PublicKey();
            Account userAcc = new Account(privatekey, publicKey);
            userAcc.PrivateKey.KeyBytes = keybytes;
            Aptos.BCS.TransactionPayload payload = GetPurchaseVPNPayload(vpnProviderObjectId);

            try
            {
                SignedTransaction signedTransaction = await aptosRestClient.CreateBCSSignedTransaction(userAcc, payload);
                ResponseInfo transactionInfo = await aptosRestClient.SubmitBCSTransaction(signedTransaction);
                string responseString = transactionInfo.message;
                if (transactionInfo.status == ResponseInfo.Status.Failed)
                {
                    MessageBox.Show("unable to submit Aptos transaction");
                    return null;
                }
                Transaction finalTransaction = JsonConvert.DeserializeObject<Transaction>(responseString, new TransactionConverter());
                if (finalTransaction != null)
                {
                    bool isPendingOrFailed = await aptosRestClient.WaitForTransaction(finalTransaction.Hash);
                    if (!isPendingOrFailed)
                    {
                        return finalTransaction.Hash;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to submit Aptos transaction, please try again");
            }
            return null;
        }

        private static Aptos.BCS.TransactionPayload GetPurchaseVPNPayload(string vpnProviderObjectId)
        {
            ISerializable[] transactionArguments =
            {
                AccountAddress.FromHex(vpnProviderObjectId)
            };
            EntryFunction payload = EntryFunction.Natural(
                new ModuleId(AccountAddress.FromHex(vpnPackageId), "vpn_manager"),  // Package ID and Module Name.
                "purchase",  // Function Name.
                new TagSequence(new ISerializableTag[] { new StructTag(AccountAddress.FromHex("0x1"), "aptos_coin", "AptosCoin", new ISerializableTag[0]) }),  // Type Arguments.
                new Sequence(transactionArguments)  // Arguments.
            );
            return new Aptos.BCS.TransactionPayload(payload);
        }

        public static string createNewAccount()
        {
            Account userAcc = new Account();
            return userAcc.PrivateKey.Key;
        }

        public static string getHexSignature(string toSign, string privateKeyStr)
        {
            PrivateKey privatekey = new PrivateKey(privateKeyStr);
            var keybytes = privatekey.KeyBytes;
            Aptos.Accounts.PublicKey publicKey = privatekey.PublicKey();
            Account userAcc = new Account(privatekey, publicKey);
            userAcc.PrivateKey.KeyBytes = keybytes;
            byte[] toSignByte = Encoding.ASCII.GetBytes(toSign);
            return userAcc.Sign(toSignByte).ToString();
        }

        public static string getPublicKey()
        {
            string privateKeyStr = LiteDBAccessor.getPrivatekey();
            PrivateKey privatekey = new PrivateKey(privateKeyStr);
            if (privateKeyStr == null)
            {
                return "setting up account";
            }
            Aptos.Accounts.PublicKey publicKey = privatekey.PublicKey();
            Account userAcc = new Account(privatekey, publicKey);
            return userAcc.AccountAddress.ToString();
        }

        public static async void requestGasFromFaucet()
        {
            string address = getPublicKey();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://faucet.testnet.aptoslabs.com/mint?amount=10000000&address=" + address),
            };
            using (var response = await client.SendAsync(request))
            {
                //response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }

        public static async Task<string> getAptQuantity()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.testnet.aptoslabs.com/v1/accounts/" + getPublicKey() +  "/resources"),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                JArray array = JArray.Parse(body);
                foreach (JObject obj in array.Children<JObject>())
                {
                    if(obj["type"].ToString() == "0x1::coin::CoinStore<0x1::aptos_coin::AptosCoin>")
                    {
                        return obj["data"]["coin"]["value"].ToString();
                    }
                }
                return "0";
            }
            /*
             * [{
                "type": "0x1::account::Account",
                "data":{"authentication_key": "0xbd2497fc645660a5c4a3e0739c0d5fb7ca68e2f717d2ac35280054c907b1db6e", "coin_register_events":{"counter": "1",…}
                },
                {
                "type": "0x1::coin::CoinStore<0x1::aptos_coin::AptosCoin>",
                "data":{
                "coin":{
                "value": "299942300"
                },
                "deposit_events":{"counter": "4", "guid":{"id":{"addr": "0xbd2497fc645660a5c4a3e0739c0d5fb7ca68e2f717d2ac35280054c907b1db6e",…},
                "frozen": false,
                "withdraw_events":{"counter": "1", "guid":{"id":{"addr": "0xbd2497fc645660a5c4a3e0739c0d5fb7ca68e2f717d2ac35280054c907b1db6e",…}
                }
                }]
            */
        }
    }
}
