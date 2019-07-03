using System;
using System.Collections.Generic;

namespace Entities
{ 
    public abstract class Account
    {
        public int AccountID { get; set; }
        public int customerID { get; set; }
        public double interestRate { get; set; }
        public List<String> transactionLog = new List<String>();

    }
}
