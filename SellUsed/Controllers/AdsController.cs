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
    [Authorize]
    public class AdsController : Controller
    {
        private ProductDb _productDb = new ProductDb();
        //private ProductVisitDb _productVisitDb = new ProductVisitDb();
        private int pageSize = 500;

        // GET: Products
        public ActionResult Index(int page = 1)
        {
            return View();
        }

        public ActionResult ShowAdsOn(int page = 1)
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Product> curProducts = _productDb.Products.Where(x => x.UserId == userId).Where(x => x.Status).OrderByDescending(x => x.Createtime).ThenByDescending(x => x.ProductId);
            int totalItems = curProducts.Count();
            Pager pager = new Pager(totalItems, page, pageSize);
            AdsListViewModel model = new AdsListViewModel
            {
                ViewName = "ShowAdsOn",
                Products = curProducts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return PartialView(model);
        }

        public ActionResult ShowAdsOff(int page = 1)
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Product> curProducts = _productDb.Products.Where(x => x.UserId == userId).Where(x => x.Status == false).OrderByDescending(x => x.Createtime).OrderByDescending(x => x.ProductId);

            int totalItems = curProducts.Count();
            Pager pager = new Pager(totalItems, page, pageSize);
            AdsListViewModel model = new AdsListViewModel
            {
                ViewName = "ShowAdsOff",
                Products = curProducts.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };
            return PartialView(model);
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

            int visit = _productDb.ProductVisits.Find(id).VisitTimes;
            if (visit != 0)
            {
                visit++;
                _productDb.ProductVisits.Find(id).VisitTimes = visit;
                await _productDb.SaveChangesAsync();
            }

            ProductViewModel model = new ProductViewModel
            {
                Product = product,
                VisitTime = visit,
                ifFavorite = _productDb.UserFavoriteAds.Where(x => x.UserId == myUserId).Any(x => x.ProductId == id)
            };
            
            return View(model);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,UserId,Name,Description,Price,Category,Createtime,StreetNo,StreetRoute,Suburb,State,Postcode,Status")] Product product)
        {
            product.UserId = User.Identity.GetUserId();
            product.Createtime = DateTime.Now;
            product.Status = true;
            //product;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _productDb.Products.Add(product);
                await _productDb.SaveChangesAsync();

                ProductVisit pv = new ProductVisit
                {
                    ProductId = product.ProductId,
                    VisitTimes = 0
                };
                _productDb.ProductVisits.Add(pv);
                await _productDb.SaveChangesAsync();

                TempData["productId"] = product.ProductId;
                return RedirectToAction("Create", "Image");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productDb.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,UserId,Name,Description,Price,Category,Createtime,StreetNo,StreetRoute,Suburb,State,Postcode")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productDb.Entry(product).State = EntityState.Modified;
                await _productDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productDb.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Product product = await _productDb.Products.FindAsync(id);
            _productDb.Products.Remove(product);
            await _productDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ChangeStatus(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productDb.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/ChangeStatus
        [HttpPost, ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeStatusConfirmed(Guid id)
        {
            Product product = await _productDb.Products.FindAsync(id);
            Boolean status = !product.Status;
            product.Status = status;
            await _productDb.SaveChangesAsync();
            return RedirectToAction("Index");
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