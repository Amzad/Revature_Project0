using System;
using System.Collections.Generic;
using Entities;
namespace DataAccessLayer
{
    public class PersonalCheckingDAL : AccountDAL
    {
        public Account Create(Customer cust)
        {
            PersonalCheckingAccount newAccount = new PersonalCheckingAccount()
            {
                AccountID = AccountDAL.accountList.Count + 1000,
                customerID = cust.ID,
                Credit = 0,
                Debit = 0,
                interestRate = 2.5

            };
            AccountDAL.accountList.Add(newAccount);
            return newAccount;
        }

        public String Withdraw(PersonalCheckingAccount pa, double amount, double balance)
        {
            pa.Credit = balance;
            pa.transactionLog.Add("Withdrawal of " + amount);
            return ($"Your new balance for account {pa.AccountID} is ${pa.Credit}");

        }

        public String Deposit(PersonalCheckingAccount pa, double amount, double balance)
        {
            pa.Credit = balance;
            pa.transactionLog.Add("Deposit of " + amount);
            return ($"Your new balance for account {pa.AccountID} is ${pa.Credit}");
        }
    }
}

