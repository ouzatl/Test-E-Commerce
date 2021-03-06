﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Test.ECommerce.Data.Models
{
    public class Product
    {
        [Key]
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double ProductPrice { get; set; }
        public long? StockCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public Category Category { get; set; }
    }
}