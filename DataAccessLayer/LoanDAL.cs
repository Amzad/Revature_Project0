using System;
using System.Collections.Generic;
using Entities;

namespace DataAccessLayer
{
    public class LoanDAL : AccountDAL
    { 
        public LoanDAL()
        {
        }

        public Account Create(Customer cust, int amount)
        {
            LoanAccount newAccount = new LoanAccount()
            {
                AccountID = AccountDAL.accountList.Count + 1000,
                customerID = cust.ID,
                Debit = amount,
                interestRate = 6.5

            };
            AccountDAL.accountList.Add(newAccount);
            return newAccount;
        }

        public String PayInstallment(LoanAccount account, double amount)
        {
            account.Debit = amount;
            account.transactionLog.Add("Payment of " + amount);
            return $"A payment of {amount} has been made. Your new loan balance is {account.Debit}";
        }
    }
}
