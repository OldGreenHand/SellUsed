using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellUsed.DataContexts;
using Microsoft.AspNet.Identity;
using SellUsed.Models;
using Microsoft.AspNet.Identity.Owin;
using SellUsed.ViewModels;

namespace SellUsed.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDb _productDb = new ProductDb();
        //private ProductVisitDb _productVisitDb = new ProductVisitDb();
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

        public ActionResult ElectricsView(int page = 1, int pageSize = 5)
        {
            string myUserId = User.Identity.GetUserId();
            IEnumerable<Product> curProducts = _productDb.Products.Where(x => x.Category == Category.Electrics).Where(x => x.Status).OrderByDescending(x => x.Createtime);
            int totalItems = curProducts.Count();
            Pager pager = new Pager(totalItems, page, pageSize);
            IEnumerable<Product> products = curProducts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            Queue<string> usernames = new Queue<string>(pageSize);
            foreach (Product p in products)
            {
                string username = UserManager.FindById(p.UserId).UserName;
                usernames.Enqueue(username);
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                ViewName = "ElectricsView",
                Products = products,
                Pager = pager,
                Username = usernames,
                FavoriteAds = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId)
            };
            return View(model);
        }

        public ActionResult CarsView(int page = 1, int pageSize = 5)
        {
            string myUserId = User.Identity.GetUserId();
            IEnumerable<Product> curProducts = _productDb.Products.Where(x => x.Category == Category.Cars).Where(x => x.Status).OrderByDescending(x => x.Createtime);
            int totalItems = curProducts.Count();
            Pager pager = new Pager(totalItems, page, pageSize);
            IEnumerable<Product> products = curProducts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            Queue<string> usernames = new Queue<string>(pageSize);
            foreach (Product p in products)
            {
                string username = UserManager.FindById(p.UserId).UserName;
                usernames.Enqueue(username);
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                ViewName = "CarsView",
                Products = products,
                Pager = pager,
                Username = usernames,
                FavoriteAds = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId)
            };
            return View(model);
        }

        public ActionResult JobsView(int page = 1, int pageSize = 5)
        {
            string myUserId = User.Identity.GetUserId();
            IEnumerable<Product> curProducts = _productDb.Products.Where(x => x.Category == Category.Jobs).Where(x => x.Status).OrderByDescending(x => x.Createtime);
            int totalItems = curProducts.Count();
            Pager pager = new Pager(totalItems, page, pageSize);
            IEnumerable<Product> products = curProducts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            Queue<string> usernames = new Queue<string>(pageSize);
            foreach (Product p in products)
            {
                string username = UserManager.FindById(p.UserId).UserName;
                usernames.Enqueue(username);
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                ViewName = "JobsView",
                Products = products,
                Pager = pager,
                Username = usernames,
                FavoriteAds = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId)
            };
            return View(model);
        }
        
        // GET: Products/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            string myUserId = User.Identity.GetUserId();
            Product product = await _productDb.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //++product.VisitTimes;
            await _productDb.SaveChangesAsync();

            var productVisit = _productDb.ProductVisits.Find(id);
            if (productVisit == null)
            {
                ProductVisit pv = new ProductVisit
                {
                    ProductId = product.ProductId,
                    VisitTimes = 0
                };
                _productDb.ProductVisits.Add(pv);
                await _productDb.SaveChangesAsync();
                productVisit = _productDb.ProductVisits.Find(id);
            }
            int visit = productVisit.VisitTimes;
            productVisit.VisitTimes = ++visit;
            await _productDb.SaveChangesAsync();

            ProductViewModel model = new ProductViewModel
            {
                Product = product,
                VisitTime = visit,
                ifFavorite = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId).Any(x => x.ProductId == id)
            };

            TempData["Username"] = UserManager.FindById(product.UserId).UserName;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
