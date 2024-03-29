﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebProjekat.DTO.OrderDTO;
using WebProjekat.Interfaces;
using WebProjekat.Services;

namespace WebProjekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("newOrder")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult CreateOrder(OrderDTO order)
        {
            order.CustomerID = User.Identity.Name;
            order.TimeOfOrder = DateTime.Now;
            if (_orderService.NewOrder(order))
                return Ok("Order is placed");
            return BadRequest("One or more products is unavailable, please refresh");
        }
        [HttpDelete("{orderID}")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult CancelOrder(string orderID)
        {
            if (!Int32.TryParse(orderID, out int id))
                return BadRequest("Invalid orderID");
            if (!_orderService.CancelOrder(id, User.Identity.Name, out string message))
                return BadRequest(message);
            return Ok(message);
        }

        [HttpGet("{orderID}")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult GetOrder(string orderID)
        {
            if (!Int32.TryParse(orderID, out int id))
                return BadRequest("Invalid order");

            var order = _orderService.GetOrder(id, out string message);

            if (order == null)
                return NotFound(message);

            if (!order.CustomerID.Equals(User.Identity.Name))
                return BadRequest("You can only view details of your orders");

            return Ok(order);
        }

        [HttpGet("admin/{orderID}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAdminOrder(string orderID)
        {
            if (!Int32.TryParse(orderID, out int id))
                return BadRequest("Invalid order");

            var order = _orderService.GetOrder(id, out string message);

            if (order == null)
                return NotFound(message);

            return Ok(order);
        }

        [HttpGet("seller/{orderID}")]
        [Authorize(Roles = "SELLER")]
        public IActionResult GetOrderSeller(string orderID)
        {
            if (!Int32.TryParse(orderID, out int id))
                return BadRequest("Invalid order");
            var order = _orderService.GetOrderSeller(id, User.Identity.Name, out string message);
            if (order == null)
                return NotFound(message);
            return Ok(order);
        }

        [HttpGet("delivered")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult GetDelievered()
        {
            return Ok(_orderService.GetDeliveredOrders(User.Identity.Name));
        }

        [HttpGet("pending")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult GetPending()
        {
            return Ok(_orderService.GetPendingOrders(User.Identity.Name));
        }

        [HttpGet("canceled")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult GetCanceled()
        {
            return Ok(_orderService.GetCanceledOrders(User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }

        [HttpGet("deliveredSeller")]
        [Authorize(Roles = "SELLER")]
        public IActionResult GetDelieveredSeller()
        {
            var orders = _orderService.GetDeliveredBySeller(User.Identity.Name);
            if (orders == null)
                return NotFound("No orders");
            return Ok(orders);
        }

        [HttpGet("pendingSeller")]
        [Authorize(Roles = "SELLER")]
        public IActionResult GetPendingSeller()
        {
            var orders = _orderService.GetPendingBySeller(User.Identity.Name);
            if (orders == null)
                return NotFound("No orders");
            return Ok(orders);
        }

        [HttpGet("canceledSeller")]
        [Authorize(Roles = "SELLER")]
        public IActionResult GetCanceledSeller()
        {
            var orders = _orderService.GetCanceledSeller(User.Identity.Name);
            if (orders == null)
                return NotFound("No orders");
            return Ok(orders);
        }
    }
}
