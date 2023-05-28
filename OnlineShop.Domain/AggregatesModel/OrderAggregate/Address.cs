using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.OrderAggregate
{
    public class Address: ValueObject
    {
        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public string ZipCode { get; private set; }

        private Address() { }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            //if (string.IsNullOrEmpty(street))
            //    throw new ArgumentException("street is missing");

            //if (string.IsNullOrEmpty(city))
            //    throw new ArgumentException("city is missing");

            //if (string.IsNullOrEmpty(state))
            //    throw new ArgumentException("state is missing");

            //if (string.IsNullOrEmpty(country))
            //    throw new ArgumentException("country is missing");

            //if (string.IsNullOrEmpty(zipcode))
            //    throw new ArgumentException("zipcode is missing");

            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
