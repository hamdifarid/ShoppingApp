using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.DTO
{
    public class CartDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }



    }
}