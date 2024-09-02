using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoUI.Models;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;

namespace DemoUI.Accessors
{
    public static class VPNProviderAccessor
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public static async Task<string> getVPNConnection(string address, string transactionHash)
        {
            string privateKey = LiteDBAccessor.getPrivatekey();
            string hexSignature = AptosAccessor.getHexSignature(transactionHash, privateKey);
            var dataToPost = JsonConvert.SerializeObject(new
            {
                signature = hexSignature,
                transactionHash = transactionHash,
            });
            var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");
            var apiData = await httpClient.PostAsync(address + "/provideAccess", content);
            if (apiData != null && apiData.IsSuccessStatusCode)
            {
                var processedData = apiData.Content.ReadAsAsync<string>();
                processedData.Wait();
                return processedData.Result;
            }
            return "";
        }
        
    }
}
