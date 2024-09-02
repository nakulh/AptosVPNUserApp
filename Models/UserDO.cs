using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUI.Models
{
    public class UserDO
    {
        public string privateKey { get; set; }
        public VPNProvider lastConnectedVPN {  get; set; }
        public DateTime vpnExpiryTime { get; set; }
        public string lastTransactionHash { get; set; }
    }
}
