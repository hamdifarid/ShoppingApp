﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public double productPrice { get; set; }
    }
}