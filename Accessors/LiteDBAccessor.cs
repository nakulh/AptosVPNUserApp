using DemoUI.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUI.Accessors
{
    public static class LiteDBAccessor
    {
        private static readonly string dbPath = "C:\\AptosVPN.db";
        public static void updateUserInfo(UserDO user)
        {
            using (var db = new LiteDatabase(@dbPath))
            {
                var col = db.GetCollection<UserDO>("user");
                col.Update(user);
            }
        }

        public static UserDO getUserInfo()
        {
            using (var db = new LiteDatabase(@dbPath))
            {
                ILiteCollection<UserDO> col = db.GetCollection<UserDO>("user");
                List<UserDO> res = col.Query().Where(x => x.privateKey.StartsWith("0x")).ToList();
                return res.FirstOrDefault();
            }
        }

        public static void insertUserInfo(UserDO user)
        {
            using (var db = new LiteDatabase(@dbPath))
            {
                var col = db.GetCollection<UserDO>("user");
                col.Insert(user);
            }
        }

        public static string getPrivatekey()
        {
            return getUserInfo().privateKey;
        }
    }
}
