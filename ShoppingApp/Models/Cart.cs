using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class Cart
    {
        public int cartId { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
    }
}