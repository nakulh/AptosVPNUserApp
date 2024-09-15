using DemoUI.Accessors;
using DemoUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUI.helpers
{
    internal static class wireguardConnectionHelper
    {
        private static readonly string connectionStringDivider = "\n";
        public static async Task<string> getVPNConnectionString(VPNProvider vpnProvider)
        {
            if (vpnProvider == null)
            {
                return "";
            }
            string connectionString = "";
            UserDO user = LiteDBAccessor.getUserInfo();
            string vpnIp = vpnProvider.address.Split(':')[0] + ":5000"; //assuming 5000 port, this should be made configurable in the future but lots of changes needed
            if (user.lastConnectedVPN != null && user.lastConnectedVPN.objectId == vpnProvider.objectId && DateTime.Compare(DateTime.Now, user.vpnExpiryTime) < 0)
            {
                Console.WriteLine("connecting to previous vpn");
                connectionString = await getConnectionString(user.lastTransactionHash, vpnIp);
            }
            else
            {
                string transactionHash = await AptosAccessor.purchaseVPN(vpnProvider.objectId, user.privateKey);
                connectionString = await getConnectionString(transactionHash, vpnIp);
            }
            return modifyConnectionString(connectionString, vpnProvider.address);
        }

        private static async Task<string> getConnectionString(string transactionHash, string address)
        {
            // first sign transaction
            return await VPNProviderAccessor.getVPNConnection(address, transactionHash);
        }

        private static string modifyConnectionString(string connectionString, string vpnProviderAddress) 
        {
            //connectionString = "[Interface]\r\nPrivateKey = 8HleSlniBAtPV2XEHOWbsfPJA0B+geu/5adTVmt1W2c=\r\nAddress = 10.138.75.3/24,fd11:5ee:bad:c0de::a8a:4b03/64\r\nDNS = 9.9.9.9, 149.112.112.112\r\n\r\n[Peer]\r\nPublicKey = bx5O3r5OXpNh+PN5rGgqihDNKYsExZ6+DEh9iDOYo0A=\r\nPresharedKey = 3TYyc8EYLy2ph7KM3pJyJ1Tl+xptrdfMsIL9dh8J2KU=\r\nEndpoint = 182.65.16.36:51820\r\nAllowedIPs = 0.0.0.0/0, ::0/0";
            //vpnProviderAddress = "123.321.456.76:6000";
            string[] connectionStringArr = connectionString.Split(connectionStringDivider);
            connectionStringArr[8] = "Endpoint = " + getVPNProviderIPAddress(vpnProviderAddress) + ":" + getVPNProviderWireguardPort(connectionStringArr[8]);
            return String.Join('\n', connectionStringArr);
        }

        private static string getVPNProviderWireguardPort(string connectionStringEndpoint)
        {
            return connectionStringEndpoint.Split(':')[1];
        }

        private static string getVPNProviderIPAddress(string vpnProviderAddress) 
        {
            return vpnProviderAddress.Split(':')[0];
        }

    }
}
