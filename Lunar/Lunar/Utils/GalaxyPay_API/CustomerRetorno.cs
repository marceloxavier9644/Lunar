using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.GalaxyPay_API
{
    public class CustomerRetorno
    {


        public class CustomerGalaxyPay
        {
            public bool type { get; set; }
            public Customer Customer { get; set; }
        }

        public class Customer
        {
            public string myId { get; set; }
            public int galaxPayId { get; set; }
            public string name { get; set; }
            public string document { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
            public string[] emails { get; set; }
            public long[] phones { get; set; }
            public Address Address { get; set; }
            public object[] ExtraFields { get; set; }
        }

        public class Address
        {
            public string zipCode { get; set; }
            public string street { get; set; }
            public string number { get; set; }
            public object complement { get; set; }
            public string neighborhood { get; set; }
            public string city { get; set; }
            public string state { get; set; }
        }

    }
}
