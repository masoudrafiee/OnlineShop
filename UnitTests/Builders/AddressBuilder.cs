using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
   public class AddressBuilder
    {
        private Address _address;
        public string TestStreet => "motahari";
        public string TestCity => "Tehran";
        public string TestState => "2";
        public string TestCountry => "ir";
        public string TestZipCode => "44240-1232";

        public AddressBuilder()
        {
            _address = WithDefaultValues();
        }
        public Address Build()
        {
            return _address;
        }
        public Address WithDefaultValues()
        {
            _address = new Address(TestStreet, TestCity, TestState, TestCountry, TestZipCode);
            return _address;
        }
    }
}
