using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using WebProjekat.Common;
using WebProjekat.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WebProjekat.Services
{
    public class DeliveryTimeService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DeliveryTimeService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<DbContextWP>();

                    var date = DateTime.Now;
                    var orders = dbContext.Orders.Where(x => x.OrderStatus == EOrderStatus.IN_PROGRESS).ToList();
                    foreach (var order in orders)
                    {
                        if (DateTime.Compare(date, order.DeliveryTime) > 0)
                            order.OrderStatus = EOrderStatus.DELIVERED;
                    }

                    dbContext.SaveChanges();

                    await Task.Delay(60000, stoppingToken);
                }
            }

            await Task.CompletedTask;
        }
    }
}
