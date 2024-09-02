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

            String transHash = "";

            aptosRestClient.CreateBCSSignedTransaction((signedTransaction) =>
            {
                aptosRestClient.SubmitBCSTransaction(async (responseString, responseInfo) =>
                {
                    Transaction finalTransaction = JsonConvert.DeserializeObject<Transaction>(responseString, new TransactionConverter());
                    if (finalTransaction != null) {
                        bool isPendingOrFailed = await aptosRestClient.WaitForTransaction(finalTransaction.Hash);
                        if ( !isPendingOrFailed)
                        {
                            transHash = finalTransaction.Hash;
                        }
                    }
                }, signedTransaction);
            }, userAcc, payload);
            return transHash;
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
            return userAcc.Sign(Encoding.ASCII.GetBytes(toSign)).ToString();
        }
    }
}
