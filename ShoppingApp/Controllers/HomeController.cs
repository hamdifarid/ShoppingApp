using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private ProductContext db = new ProductContext();
        public ActionResult Index()
        {
            List<Product> products = db.Products.ToList();
            return View(products);
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

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            string productName = product.productName;
            string productDescription = product.productDescription;
            double productPrice = product.productPrice;
            db.Products.Add(new Product { productName = productName, productDescription = productDescription, productPrice = productPrice });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}