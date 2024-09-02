using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DemoUI.Models;

namespace DemoUI.Accessors
{
    public static class VPNIndexerAccessor
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string vpnIndexerAddress = "http://localhost:3000/";
        public static async Task<List<VPNProvider>> GetVPNProviders()
        {
            var apiData = await httpClient.GetAsync(vpnIndexerAddress + "getListings");
            if (apiData != null && apiData.IsSuccessStatusCode) {
                var processedData = apiData.Content.ReadAsAsync<List<VPNProvider>>();
                processedData.Wait();
                return processedData.Result;
            }
            return new List<VPNProvider>();
        }
    }
}
