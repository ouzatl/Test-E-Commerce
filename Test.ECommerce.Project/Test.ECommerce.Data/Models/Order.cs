using System;
using System.ComponentModel.DataAnnotations;

namespace Test.ECommerce.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string BasketJson { get; set; }
        public DateTime CreateDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
