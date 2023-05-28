using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Exceptions
{
    public class DomainError
    { 
        public string Error { get; set; }

        public DomainError(string error)
        {
            Error = error;
        }
    }
}
