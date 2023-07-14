using AutoMapper;
using System;
using System.Collections.Generic;
using WebProjekat.Common;
using WebProjekat.DTO.OrderDTO;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
    public class OrderService : IOrderService
    {
        private readonly double _shipping = 5.5;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public bool NewOrder(OrderDTO order)
        {
            Random rand = new();
            int orderID = rand.Next();
            double price = 0;
            Product product = null;
            List<Product> products = new();
            foreach (var orderItem in order.OrderedProducts)
            {
                product = _productRepository.GetProduct(orderItem.ProductID);
                if (product == null || product.Amount < orderItem.Quantity)
                    return false;

                orderItem.OrderID = orderID;
                price += product.Price * orderItem.Quantity;
                product.Amount -= orderItem.Quantity;
                products.Add(product);
            }

            List<OrderItem> orderItems = _mapper.Map<List<OrderItem>>(order.OrderedProducts);

            Order newOrder = _mapper.Map<Order>(order);
            newOrder.OrderedProducts = orderItems;
            newOrder.OrderStatus = EOrderStatus.IN_PROGRESS;
            newOrder.Price = price + _shipping;

            int days = rand.Next(0, 5);
            int hours = rand.Next(1, 24);
            int minutes = rand.Next(1, 60);

            newOrder.DeliveryTime = newOrder.TimeOfOrder.AddDays(days);
            newOrder.DeliveryTime.AddHours(hours);
            newOrder.DeliveryTime.AddMinutes(minutes);

            return _orderRepository.NewOrder(newOrder, products);
        }

        public bool CancelOrder(int orderID, string customerID, out string message)
        {
            var order = _orderRepository.GetOrderByID(orderID);
            if (order == null)
            {
                message = "Order doesn't exist";
                return false;
            }

            if (!order.CustomerID.Equals(customerID))
            {
                message = "You can only cancel your orders";
                return false;
            }

            var timeSpan = DateTime.Now - order.TimeOfOrder;
            if (timeSpan.Days > 0 || timeSpan.Hours >= 1)
            {
                message = "You can only cancel orders within one hour of ordering";
                return false;
            }

            var orderedItems = _orderRepository.GetOrderItems(orderID);
            List<Product> products = new();

            foreach (var orderItem in orderedItems)
            {
                var product = _productRepository.GetProduct(orderItem.ProductID);
                if (product != null)
                {
                    product.Amount += orderItem.Quantity;
                    products.Add(product);
                }
            }

            order.OrderStatus = EOrderStatus.CANCELED;
            var res = _orderRepository.CancelOrder(order, products);

            if (res)
            {
                message = "success";
                return true;
            }
            message = "Something went wrong";
            return false;
        }

        public OrderDetailsDTO GetOrder(int orderID, out string message)
        {
            var order = _orderRepository.GetOrderByID(orderID);
            if (order == null)
            {
                message = "Order doesn't exist";
                return null;
            }

            OrderDetailsDTO info = _mapper.Map<OrderDetailsDTO>(order);
            info.Products = new List<OrderItemDetailsDTO>();

            var orderItems = _orderRepository.GetOrderItems(orderID);

            foreach (var orderItem in orderItems)
            {
                var product = _productRepository.GetProduct(orderItem.ProductID);
                var productDetails = _mapper.Map<OrderItemDetailsDTO>(product);
                productDetails.Quantity = orderItem.Quantity;
                info.Products.Add(productDetails);
            }
            message = "Success";
            return info;
        }

        public List<OrderDTO> GetDeliveredOrders(string customerID)
        {
            return GetByCriteria(customerID, EOrderStatus.DELIVERED);
        }

        public List<OrderDTO> GetPendingOrders(string customerID)
        {
            return GetByCriteria(customerID, EOrderStatus.IN_PROGRESS);
        }

        public List<OrderDTO> GetCanceledOrders(string customerID)
        {
            return GetByCriteria(customerID , EOrderStatus.CANCELED);
        }

        public List<OrderDTO> GetOrders()
        {
            var orders = _orderRepository.GetOrders();
            List<OrderDTO> retOrders = new();

            foreach (var order in orders)
            {
                var mapped = _mapper.Map<OrderDTO>(order);
                mapped.OrderedProducts = _mapper.Map<List<OrderItemDTO>>(_orderRepository.GetOrderItems(order.OrderID));
                retOrders.Add(mapped);
            }

            return retOrders;
        }

        private List<OrderDTO> GetByCriteria(string customerID, EOrderStatus status)
        {
            var orders = _orderRepository.GetByStatus(customerID, status);
            List<OrderDTO> retOrders = new();

            foreach (var order in orders)
            {
                var mapped = _mapper.Map<OrderDTO>(order);
                mapped.OrderedProducts = _mapper.Map<List<OrderItemDTO>>(_orderRepository.GetOrderItems(order.OrderID));
                retOrders.Add(mapped);
            }

            return retOrders;
        }

        public List<OrderDTO> GetDeliveredBySeller(string sellerID)
        {
            return GetBySeller(sellerID, EOrderStatus.DELIVERED);
        }

        private List<OrderDTO> GetBySeller(string sellerID, EOrderStatus status)
        {
            var productIDs = _productRepository.GetProductsBySeller(sellerID);
            if (productIDs == null)
                return null;

            var orderIDs = _orderRepository.GetOrderItemsByProductIDs(productIDs);
            if (orderIDs == null)
                return null;

            var orders = _orderRepository.GetOrdersBySeller(orderIDs, status);

            List<OrderDTO> retOrders = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var mapped = _mapper.Map<OrderDTO>(order);
                mapped.OrderedProducts = _mapper.Map<List<OrderItemDTO>>(_orderRepository.GetOrderItemsBySeller(order.OrderID, productIDs));
                retOrders.Add(mapped);
            }

            return retOrders;
        }

        public List<OrderDTO> GetPendingBySeller(string sellerID)
        {
            return GetBySeller(sellerID, EOrderStatus.IN_PROGRESS);
        }

        public List<OrderDTO> GetCanceledSeller(string sellerID)
        {
            return GetBySeller(sellerID, EOrderStatus.CANCELED);
        }

        public OrderDetailsDTO GetOrderSeller(int orderID, string sellerID, out string message)
        {
            var productIDs = _productRepository.GetProductsBySeller(sellerID);
            if (productIDs == null)
            {
                message = "You have no products";
                return null;
            }

            var order = _orderRepository.GetOrderByID(orderID);
            if (order == null)
            {
                message = "Order doesn't exist";
                return null;
            }

            OrderDetailsDTO info = _mapper.Map<OrderDetailsDTO>(order);
            info.Products = new List<OrderItemDetailsDTO>();

            var orderItems = _orderRepository.GetOrderItemsBySeller(orderID, productIDs);

            foreach (var orderItem in orderItems)
            {
                var product = _productRepository.GetProduct(orderItem.ProductID);
                var productDetails = _mapper.Map<OrderItemDetailsDTO>(product);
                productDetails.Quantity = orderItem.Quantity;
                info.Products.Add(productDetails);

            }
            if(info.Products.Count == 0)
            {
                message = "You have no products in this order";
                return null;
            }
            message = "Success";
            return info;
        }
    }
}
