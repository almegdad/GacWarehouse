﻿using GacWarehouse.Core.Interfaces.Repositories;
using GacWarehouse.Core.Interfaces.Services;
using GacWarehouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GacWarehouse.Core.Entities;
using System.Threading.Tasks;

namespace GacWarehouse.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GeneralResponse<OrderResponse>> CreateNewOrder(OrderRequest request)
        {
            var respnose = new GeneralResponse<OrderResponse>();

            var isValid = request.OrderDetailsList?.Count > 0 &&
                request.OrderDetailsList.All(a => a?.ProductId > 0 &&
                                                  a?.Quantity > 0 &&
                                                  a.Quantity <= _orderRepository.GetProductById(a.ProductId).Quantity);
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }
             
            
            var orderModel = new SalesOrder
            {
                CustomerId = request.CustomerId,
                OrderStatus = Core.Enums.OrderStatusType.New,
                OrderCreateDate = DateTime.Now,
                SalesOrderDetails = request.OrderDetailsList.Select(a => new SalesOrderDetails
                {
                    ProductId = a.ProductId,
                    Quantity = a.Quantity
                }).ToList()
            };

            var order = _orderRepository.CreateNewOrder(orderModel);

            if (order == null)
            {
                respnose.Message = "Save Error";
                return respnose;
            }

            var orderResponse = new OrderResponse
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus,
                OrderCreateDate = order.OrderCreateDate,
                OrderDetailsList = order.SalesOrderDetails.Select(a => new OrderDetailsResponse
                {
                    ProductId = a.Product.Id,
                    ProductName = a.Product.Name,
                    Quantity = a.Quantity
                }).ToList()
            };

            respnose = new GeneralResponse<OrderResponse>()
            {
                Success = true,
                Data = orderResponse
            };

            return respnose;
        }

        public async Task<GeneralResponse<OrderResponse>> GetOrderDetails(int orderId, int customerId)
        {
            var respnose = new GeneralResponse<OrderResponse>();

            var isValid = orderId > 0 && customerId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var order = _orderRepository.GetOrderDetails(orderId, customerId);

            if (order == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            var orderResponse = new OrderResponse
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus,
                OrderCreateDate = order.OrderCreateDate,
                OrderDetailsList = order.SalesOrderDetails.Select(a => new OrderDetailsResponse
                {
                    ProductId = a.Product.Id,
                    ProductName = a.Product.Name,
                    Quantity = a.Quantity
                }).ToList()
            };

            respnose = new GeneralResponse<OrderResponse>()
            {
                Success = true,
                Data = orderResponse
            };

            return respnose;
        }
    }
}
