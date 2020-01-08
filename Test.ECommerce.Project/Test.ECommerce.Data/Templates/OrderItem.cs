using System;
using System.Collections.Generic;
using System.Text;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Templates
{
    public class OrderItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
