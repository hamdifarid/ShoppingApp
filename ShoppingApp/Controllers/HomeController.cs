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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            Product product = await db.Products.FindAsync(productId);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {
            // Retrieve the product from the database
            Product product = await db.Products.FindAsync(productId);

            if (product != null)
            {
                // Add product to the shopping cart
                // Here you can implement your logic to add the product to the shopping cart
                // You might use session, database, or any other storage mechanism to manage the cart
                // For demonstration purposes, I'll simply redirect to the "AddToCart" view
                return View("AddToCart");
            }
            else
            {
                TempData["Message"] = "Product not found";
                return RedirectToAction("Index");
            }
        }

    }
}