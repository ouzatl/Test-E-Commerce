using System;

namespace Test.ECommerce.Data.Contract
{
    public class BasketContract
    {
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