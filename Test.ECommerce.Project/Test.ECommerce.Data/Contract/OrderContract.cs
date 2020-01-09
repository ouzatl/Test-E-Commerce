using System;

namespace Test.ECommerce.Data.Contract
{
    public class OrderContract
    {
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