using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellUsed.Models
{
    public class OnlineUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime OnlineTime { get; set; }
    }
}