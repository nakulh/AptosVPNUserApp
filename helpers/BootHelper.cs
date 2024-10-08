﻿using AptosVPNClient.Accessors;
using AptosVPNClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptosVPNClient.helpers
{
    public static class BootHelper
    {
        public static async Task BootVPNClient()
        {
            UserDO user = LiteDBAccessor.getUserInfo();
            if (user == null || string.IsNullOrEmpty(user.privateKey))
            {
                Console.WriteLine("First boot so creating new user");
                await firstBootVPNClient();
            }
        }

        private static async Task firstBootVPNClient()
        {
            string privateKey = AptosAccessor.createNewAccount();
            UserDO user = new UserDO();
            user.privateKey = privateKey;
            LiteDBAccessor.insertUserInfo(user);
            await AptosAccessor.requestGasFromFaucet();
        }
    }
}
