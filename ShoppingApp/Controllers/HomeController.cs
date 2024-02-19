using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private ProductContext db = new ProductContext();
        public async Task<ActionResult> Index()
        {
            List<Product> products = await db.Products.ToListAsync();

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
        public async Task<ActionResult> AddProduct(Product product)
        {
            string productName = product.productName;
            string productDescription = product.productDescription;
            double productPrice = product.productPrice;
            db.Products.Add(new Product { productName = productName, productDescription = productDescription, productPrice = productPrice });
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}