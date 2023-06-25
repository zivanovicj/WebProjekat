using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebProjekat.DTO.OrderDTO;
using WebProjekat.Interfaces;

namespace WebProjekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "CUSTOMER")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("newOrder")]
        public IActionResult CreateOrder(OrderDTO order)
        {
            order.CustomerID = User.Identity.Name;
            order.TimeOfOrder = DateTime.Now;
            if (_orderService.NewOrder(order))
                return Ok("Order is placed");
            return BadRequest("One or more products is unavailable, please refresh");
        }
    }
}
