using System.Collections.Generic;

namespace Test.ECommerce.Data.Templates
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string Description { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}