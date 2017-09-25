using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SellUsed
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    //public class TrackLoginsFilter : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        Dictionary<string, DateTime> loggedInUsers = SecurityHelper.GetLoggedInUsers();

    //        if (HttpContext.Current.User.Identity.IsAuthenticated)
    //        {
    //            if (loggedInUsers.ContainsKey(HttpContext.Current.User.Identity.GetUserId()))
    //            {
    //                loggedInUsers[HttpContext.Current.User.Identity.GetUserId()] = NtpTime.GetNetworkTime();
    //            }
    //            else
    //            {
    //                loggedInUsers.Add(HttpContext.Current.User.Identity.GetUserId(), NtpTime.GetNetworkTime());
    //            }

    //        }

    //        // remove users where time exceeds session timeout
    //        var keys = loggedInUsers.Where(u => NtpTime.GetNetworkTime().AddMinutes(-HttpContext.Current.Session.Timeout) >
    //                                            u.Value).Select(u => u.Key);
    //        foreach (var key in keys)
    //        {
    //            loggedInUsers.Remove(key);
    //        }

    //    }
    //}
}
