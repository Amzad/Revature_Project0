using System;
using System.Collections.Generic;

namespace Entities
{
    public class Customer
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public int ID { get; set; }
        public List<int> Accounts = new List<int>();

    }
}
