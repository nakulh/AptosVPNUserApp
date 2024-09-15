using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using AptosVPNClient.Models;
using System.Windows.Forms;

namespace AptosVPNClient.Accessors
{
    public static class VPNIndexerAccessor
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string vpnIndexerAddress = "http://localhost:3000/";
        public static async Task<List<VPNProvider>> GetVPNProviders()
        {
            try
            {
                var apiData = await httpClient.GetAsync(vpnIndexerAddress + "getListings");
                if (apiData != null && apiData.IsSuccessStatusCode)
                {
                    var processedData = apiData.Content.ReadAsAsync<List<VPNProvider>>();
                    processedData.Wait();
                    return processedData.Result;
                }
            } catch (Exception ex) {
                MessageBox.Show("unable to connect with VPN indexer node");
            }
            return new List<VPNProvider>();
        }
    }
}
