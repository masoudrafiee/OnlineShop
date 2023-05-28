using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.BuyerAggregate
{
    public class Buyer:BaseEntity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string Name { get; private set; }
        public Buyer()
        {

        }
        public Buyer(string identity, string name) : this()
        {
            if (string.IsNullOrEmpty(identity))
                AddError("UserId is null");

            if (string.IsNullOrEmpty(name))
                AddError("UserName is null");

            if (Errors.Any())
                throw new DomainException(Errors.Select(e => e.Error));

            IdentityGuid = identity;
            Name = name;
        }
    }
}
