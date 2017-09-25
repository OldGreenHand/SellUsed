using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellUsed.Utility
{
    public static class OnlineUsersHelperWithCache
    {
        public static Dictionary<string, DateTime> GetLoggedInUsers()
        {
            UpdateOnlineStatus(HttpContext.Current.User.Identity.Name);
            return (Dictionary<string, DateTime>)HttpRuntime.Cache["LoggedInUsers"];
        }
        
        public static void UpdateOnlineStatus(string myUserName)
        {
            //if the list exists, add this user to it
            if (HttpRuntime.Cache["LoggedInUsers"] != null)
            {
                //get the list of logged in users from the cache
                var loggedInUsers = (Dictionary<string, DateTime>)HttpRuntime.Cache["LoggedInUsers"];
                

                if (!loggedInUsers.ContainsKey(myUserName))
                {
                    //add this user to the list
                    loggedInUsers.Add(myUserName, DateTime.Now);
                    //add the list back into the cache
                    HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
                }
                else
                {
                    loggedInUsers[myUserName] = DateTime.Now;
                    HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
                }
            }
            //the list does not exist so create it
            else
            {
                //create a new list
                var loggedInUsers = new Dictionary<string, DateTime>();
                //add this user to the list
                loggedInUsers.Add(myUserName, DateTime.Now);
                
                //add the list into the cache
                HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
            }

            //update user according to time
            if (HttpRuntime.Cache["LoggedInUsers"] != null)
            {
                var loggedInUsers = (Dictionary<string, DateTime>)HttpRuntime.Cache["LoggedInUsers"];

                var keys = loggedInUsers
                    .Where(u => DateTime.Now.AddMinutes(-HttpContext.Current.Session.Timeout) >
                                u.Value).Select(u => u.Key);
                foreach (var key in keys.ToList())
                {
                    loggedInUsers.Remove(key);
                }
                HttpRuntime.Cache["LoggedInUsers"] = loggedInUsers;
            }
        }

        public static Boolean CheckIfUserOnline(string userName)
        {
            var loggedInUsers = GetLoggedInUsers();
            return loggedInUsers != null && loggedInUsers.ContainsKey(userName);
        }
        
    }
}
