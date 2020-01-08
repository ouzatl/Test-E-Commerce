using System;
using System.ComponentModel.DataAnnotations;

namespace Test.ECommerce.Data.Models
{
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }
        public int CustomerId { get; set; }
        public string BasketKey { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}