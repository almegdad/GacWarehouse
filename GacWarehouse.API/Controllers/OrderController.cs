using GacWarehouse.Core.Interfaces.Services;
using GacWarehouse.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GacWarehouse.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateNewOrder")]
        public async Task<IActionResult> CreateNewOrder([FromBody] OrderRequest request)
        {
            var customerId = 0;
            int.TryParse(User?.Claims?.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value, out customerId);
            request.CustomerId = customerId;

            var response = await _orderService.CreateNewOrder(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
