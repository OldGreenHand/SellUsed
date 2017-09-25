using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SellUsed.DataContexts;
using SellUsed.Models;

namespace SellUsed.Utility
{
    public static class OnlineUsersHelper
    {
        private static IdentityDb _identityDb = new IdentityDb();

        public static Dictionary<string, DateTime> GetLoggedInUsers()
        {
            UpdateOnlineStatus(HttpContext.Current.User.Identity.Name);
            
            return _identityDb.OnlineUsers.ToDictionary(x => x.Username, y => y.OnlineTime);
        }

        public static void RemoveUser(string myUserName)
        {
            if (CheckIfUserOnline(myUserName))
            {
                _identityDb.OnlineUsers.Remove(_identityDb.OnlineUsers.FirstOrDefault(x => x.Username == myUserName));
                _identityDb.SaveChanges();
            }
        }

        public static void UpdateOnlineStatus(string myUserName)
        {
            if (myUserName != "")
            {
                //if the user is not in list, add this user to it
                if (!_identityDb.OnlineUsers.Any(x => x.Username == myUserName))
                {
                    OnlineUser newOnlineUser = new OnlineUser
                    {
                        Username = myUserName,
                        OnlineTime = DateTime.Now
                    };
                    _identityDb.OnlineUsers.Add(newOnlineUser);
                }
                else
                {
                    _identityDb.OnlineUsers.FirstOrDefault(x => x.Username == myUserName).OnlineTime = DateTime.Now;
                }
                _identityDb.SaveChanges();

                if (_identityDb.OnlineUsers.Count() != 0)
                {
                    //update all online user according to time
                    DateTime timeout = DateTime.Now.AddMinutes(-HttpContext.Current.Session.Timeout);
                    var usernames = _identityDb.OnlineUsers.Where(x => x.OnlineTime <= timeout).Select(x => x.Username);
                    foreach (var username in usernames.ToList())
                    {
                        _identityDb.OnlineUsers.Remove(_identityDb.OnlineUsers.FirstOrDefault(x => x.Username == username));
                        _identityDb.SaveChanges();
                    }
                }

            }
        }

        public static Boolean CheckIfUserOnline(string userName)
        {
            return _identityDb.OnlineUsers.Any(x => x.Username == userName);
        }

    }
}
