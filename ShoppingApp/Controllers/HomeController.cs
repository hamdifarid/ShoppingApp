using ShoppingApp.DTO;
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

            return View("Index",products);
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

        public async Task<ActionResult> CartPage()
        {
            //    List<int> productId = await db.Carts.Where(c=> c.UserId ==1).Select(c=>c.ProductId).ToListAsync();
            //    List<Product> products = await db.Products.Where(p => productId.Contains(p.ProductId)).ToListAsync();
            //run custom sql query
            List<CartDTO> userProducts = await db.Database.SqlQuery<CartDTO>("select p.ProductId, ProductName,ProductPrice," +
                "Quantity,(ProductPrice*Quantity) as Price from Products as p " +
                "join Carts as c on p.ProductId = c.ProductId where UserId=1; ").ToListAsync();
            return View("AddToCart", userProducts);
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
            string productName = product.ProductName;
            string productDescription = product.ProductDescription;
            double productPrice = product.ProductPrice;
            db.Products.Add(new Product { ProductName = productName, ProductDescription = productDescription, ProductPrice = productPrice });
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(int productId, Product product)
        {
            var editproduct = await db.Products.FindAsync(productId);
            editproduct.ProductName = product.ProductName;
            editproduct.ProductDescription = product.ProductDescription;
            editproduct.ProductPrice = product.ProductPrice;
            await db.SaveChangesAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId)
        {

            int productQuantity = await GetQuantityOfProduct(productId, 1);
            if (productQuantity > 0)
            {
                Carts cart = await db.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == 1);
                cart.Quantity = productQuantity + 1;
                await db.SaveChangesAsync();
                return RedirectToAction("CartPage");
            }
            else
            {
                Carts cart = db.Carts.Add(new Carts { ProductId = productId, Quantity = productQuantity + 1, UserId = 1 });
                await db.SaveChangesAsync();
                return RedirectToAction("CartPage");
            }


        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int productId)
        {
            // Find the cart item for the given product and user
            Carts cartItem = await db.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == 1);

            if (cartItem != null)
            {
                // If the quantity is greater than 1, decrement it
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    // If the quantity is 1 or less, remove the item from the cart
                    db.Carts.Remove(cartItem);
                }

                await db.SaveChangesAsync();
            }

            // Redirect back to the cart page
            return RedirectToAction("CartPage");
        }


        public async Task<int> GetQuantityOfProduct(int productId, int userId)
        {
            Carts cart = await db.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);
            if (cart != null)
            {
                return cart.Quantity;
            }
            else
            {
                return 0;
            }
        }

    }
}