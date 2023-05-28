using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.ErrorHandling
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }
    }
}
