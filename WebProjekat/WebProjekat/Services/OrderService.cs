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
        private double _shipping = 5.5;
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
    }
}
