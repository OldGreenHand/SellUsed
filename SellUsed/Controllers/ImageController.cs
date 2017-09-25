using SellUsed.DataContexts;
using SellUsed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SellUsed.ViewModels;

namespace SellUsed.Controllers
{
    public class ImageController : Controller
    {
        private ProductDb db = new ProductDb();

        public ActionResult Create()
        {
            //var model = new ImageViewModel();
            if (TempData["productId"] == null)
            {
                return Content("Your Ad is not well created or the productId is not fetched. Please try again!");
            }
            TempData["ProductId"] = (Guid)TempData["productId"];
            //model.ProductId = (int)TempData["productId"];
            //TempData["productId"] = model.ProductId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ImageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ProductImages fileUploadModel = new ProductImages();
            Guid id = (Guid)TempData["ProductId"];
            foreach (var item in model.File)
            {
                byte[] uploadFile = new byte[item.InputStream.Length];
                item.InputStream.Read(uploadFile, 0, uploadFile.Length);
                fileUploadModel.ProductId = id;
                fileUploadModel.File = uploadFile;

                db.ProductImages.Add(fileUploadModel);
                db.SaveChanges();
            }
            TempData["ImageUploadFlag"] = true;
            return RedirectToAction("Index", "Ads");
        }

        public ActionResult Index(Guid productid)
        {
            IEnumerable<ProductImages> files = db.ProductImages.Where(x => x.ProductId == productid).OrderBy(x => x.ImageId);
            return PartialView(files);
        }

        public ActionResult DisplayThumbnail(Guid productid)
        {
            ProductImages file = db.ProductImages.Where(x => x.ProductId == productid).FirstOrDefault();
            return PartialView(file);
        }
    }
}