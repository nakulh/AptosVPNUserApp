﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptosVPNClient.Models;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;

namespace AptosVPNClient.Accessors
{
    public static class VPNProviderAccessor
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public static async Task<string> getVPNConnection(string address, string transactionHash)
        {
            address = "http://" + address;
            string privateKey = LiteDBAccessor.getPrivatekey();
            string hexSignature = AptosAccessor.getHexSignature(transactionHash, privateKey);
            var dataToPost = JsonConvert.SerializeObject(new
            {
                signature = hexSignature,
                transactionHash
            });
            var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");
            var apiData = await httpClient.PostAsync(address + "/provideAccess", content);
            if (apiData != null && apiData.IsSuccessStatusCode)
            {
                var processedData = await apiData.Content.ReadAsStringAsync();
                return processedData;
            }
            return "";
        }
        
    }
}
