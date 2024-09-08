using DemoUI.Accessors;
using DemoUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUI.helpers
{
    public static class BootHelper
    {
        public static void BootVPNClient()
        {
            UserDO user = LiteDBAccessor.getUserInfo();
            if (user == null || string.IsNullOrEmpty(user.privateKey))
            {
                Console.WriteLine("First boot so creating new user");
                firstBootVPNClient();
            }
        }

        private static async void firstBootVPNClient()
        {
            string privateKey = AptosAccessor.createNewAccount();
            UserDO user = new UserDO();
            user.privateKey = privateKey;
            LiteDBAccessor.insertUserInfo(user);
            AptosAccessor.requestGasFromFaucet();
        }
    }
}
