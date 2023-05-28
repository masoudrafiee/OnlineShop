using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public class OrderTimeService : IOrderTimeService
    {
        private readonly IConfiguration _configuration;
        private readonly OrderTime _orderTime;
        public OrderTimeService(IConfiguration configuration,OrderTime orderTime)
        {
            _configuration = configuration;
            _orderTime = orderTime;
        }
        public bool IsOrderTime(TimeSpan orderTime)
        {
            var startTime = TimeSpan.Parse(_orderTime.StartTime);
            var endTime = TimeSpan.Parse(_orderTime.EndTime);
            if (orderTime < startTime || orderTime > endTime)
                return false;
            return true;
        }
    }
}
