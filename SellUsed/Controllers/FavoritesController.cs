using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SellUsed.DataContexts;
using SellUsed.Models;
using SellUsed.ViewModels;

namespace SellUsed.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly ProductDb _productDb = new ProductDb();
        //UserManager
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Favorites
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            string myUserId = User.Identity.GetUserId();
            IEnumerable<Guid> results = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId).Select(x => x.ProductId).ToList();
            int totalItems = results.Count();
            Pager pager = new Pager(totalItems, page, pageSize); Queue<string> usernames = new Queue<string>(pageSize);
            IEnumerable<Product> products = new List<Product>();
            List<Product> curProducts = new List<Product>();
            if (totalItems != 0)
            {
                foreach (var result in results)
                {
                    Product tmp = _productDb.Products.FirstOrDefault(x => x.ProductId == result);
                    curProducts.Add(tmp);
                }
                products = curProducts.OrderByDescending(x => x.Createtime).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();

            }
            foreach (Product p in products)
            {
                string username = UserManager.FindById(p.UserId).UserName;
                usernames.Enqueue(username);
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                ViewName = "MyFavoriteAds",
                Products = products,
                Pager = pager,
                Username = usernames,
                FavoriteAds = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId)
            };
            return View(model);
        }
        
        public ActionResult EditFavoriteAd(Guid productId)
        {
            string myUserId = User.Identity.GetUserId();

            UserFavoriteAd ufa = new UserFavoriteAd
            {
                UserId = myUserId,
                ProductId = productId
            };

            bool response;
            IEnumerable<UserFavoriteAd> result = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId)
                .Where(x => x.ProductId == productId);
            if (!result.Any())
            {
                _productDb.UserFavoriteAds.Add(ufa);
                response = true;
            }
            else
            {
                _productDb.UserFavoriteAds.Remove(result.FirstOrDefault());
                response = false;
            }
            _productDb.SaveChanges();
            return Json(new {ifFavorited = response});
        }
    }
}