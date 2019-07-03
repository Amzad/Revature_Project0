using System;
using Entities;
namespace DataAccessLayer
{
    public class TermDepositDAL
    {
        public Account Create(Customer cust, int amount, int length)
        {
            Account newAccount = new TermDepositAccount()
            {
                AccountID = AccountDAL.accountList.Count + 1000,
                customerID = cust.ID,
                Credit = amount,
                interestRate = 5.5,
                depositTerm = length,

            };
            AccountDAL.accountList.Add(newAccount);
            return newAccount;
        }

        public String Withdraw(TermDepositAccount ta, double amount)
        {
            ta.Credit = amount;
            ta.transactionLog.Add("Withdrawal of " + amount);
            return ($"Your new balance for account {ta.AccountID} is ${ta.Credit}");

        }

    }
}

