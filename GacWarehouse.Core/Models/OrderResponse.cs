using GacWarehouse.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GacWarehouse.Core.Models
{
    public class OrderResponse
    {
        public OrderResponse()
        {
            OrderDetailsList = new List<OrderDetailsResponse>();
        }

        public int OrderId { get; set; }
        public OrderStatusType OrderStatus { get; set; }
        public DateTime OrderCreateDate { get; set; }

        public List<OrderDetailsResponse> OrderDetailsList { get; set; }
    }

    public class OrderDetailsResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
