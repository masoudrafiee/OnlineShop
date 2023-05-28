using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public interface IOrderTimeService
    {
        bool IsOrderTime(TimeSpan orderTime);
    }
}
