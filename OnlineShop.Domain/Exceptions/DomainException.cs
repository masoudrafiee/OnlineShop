using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public IEnumerable<string> Messages { get; set; }

        public DomainException(IEnumerable<string> messages)
        {
            Messages = messages;
        }
    }
}
