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
        [HttpDelete("{orderID}")]
        public IActionResult CancelOrder(string orderID)
        {
            if (!Int32.TryParse(orderID, out int id))
                return BadRequest("Invalid orderID");
            if (!_orderService.CancelOrder(id, User.Identity.Name, out string message))
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("{orderID}")]
        public IActionResult GetOrder(string orderID)
        {
            if (!Int32.TryParse(orderID, out int id))
                return BadRequest("Invalid order");

            var order = _orderService.GetOrder(id, User.Identity.Name, out string message);

            if (order == null)
                return NotFound(message);

            if (!order.CustomerID.Equals(User.Identity.Name))
                return BadRequest("You can only view details of your orders");

            return Ok(order);
        }
    }
}
