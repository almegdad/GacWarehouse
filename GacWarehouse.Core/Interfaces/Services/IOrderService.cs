using GacWarehouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GacWarehouse.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<GeneralResponse<OrderResponse>> CreateNewOrder(OrderRequest request);
        Task<GeneralResponse<OrderResponse>> GetOrderDetails(int orderId, int customerId);
    }
}
