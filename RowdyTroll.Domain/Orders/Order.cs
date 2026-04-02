using System;
using System.Collections.Generic;

namespace RowdyTroll.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
