using SellUsed.DataContexts;
using SellUsed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SellUsed.ViewModels;

namespace SellUsed.Controllers
{
    public class HomeController : Controller
    {
        private ProductDb _productDb = new ProductDb();
        private ConversationDb _conDb = new ConversationDb();

        public int pageSize = 12;

        public ActionResult Index()
        {
            string myUserId = User.Identity.GetUserId();
            int page = 1;
            IEnumerable<Product> allProducts = _productDb.Products.Where(x => x.Status);
            List<Product> curProducts = allProducts.Where(x => x.Category == Category.Electrics).OrderByDescending(x => x.Createtime).Take(4)
                                    .Concat(allProducts.Where(x => x.Category == Category.Cars).OrderByDescending(x => x.Createtime).Take(4))
                                    .Concat(allProducts.Where(x => x.Category == Category.Jobs).OrderByDescending(x => x.Createtime).Take(4)).ToList();
            int totalItems = curProducts.Count;
            Pager pager = new Pager(totalItems, page, pageSize);
            ProductsListViewModel model = new ProductsListViewModel
            {
                ViewName = "LatestProductsView",
                Products = curProducts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager,
                FavoriteAds = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId)
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.Message = "My Dashboard";
            var userId = User.Identity.GetUserId();
            int totalUnread = 0;
            foreach (var unreadMessages in _conDb.Conversations.Where(x => x.FromUserId == userId).Where(x => x.UnreadMessageNo > 0).ToList())
            {
                totalUnread += unreadMessages.UnreadMessageNo;
            }
            DashboardViewModel vm = new DashboardViewModel
            {
                AdsNo = _productDb.Products.Count(x => x.UserId == userId),
                ContactsNo = _conDb.Conversations.Count(x => x.FromUserId == userId),
                FavoritesNo = _productDb.UserFavoriteAds.Count(x => x.UserId == userId),
                UnreadNo = totalUnread
            };
            return View(vm);
        }
    }
}