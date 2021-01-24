﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GacWarehouse.TaskScheduler.Models
{
    public class OrderRequest
    {
        public OrderRequest()
        {
            OrderDetailsList = new List<OrderDetailsRequest>();
        }
        public int CustomerId { get; set; }
        public List<OrderDetailsRequest> OrderDetailsList { get; set; }
    }

    public class OrderDetailsRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public class OrderRequestDto
    {
        public OrderRequest Root { get; set; }
    }
}
