using System;
using System.Collections.Generic;
using Entities;
namespace DataAccessLayer
{
    public class BusinessCheckingDAL : AccountDAL
    {
        public Account Create(Customer cust)
        {
            BusinessCheckingAccount newAccount = new BusinessCheckingAccount()
            {
                AccountID = AccountDAL.accountList.Count + 1000,
                customerID = cust.ID,
                Credit = 0,
                Debit = 0,
                interestRate = 4.5

            };
            AccountDAL.accountList.Add(newAccount);
            return newAccount;
        }

        public String Withdraw(BusinessCheckingAccount ba, double amount, double balance)
        {

            if (balance >= 0) // If credit is still posible after withdrawl
            {
                ba.Debit = 0;
                ba.Credit = balance;
                ba.transactionLog.Add("Withdrawal of " + amount);
                return ($"Your new balance for account {ba.AccountID} is ${ba.Credit}");
            }
            else
            {
                ba.Credit = 0;
                ba.Debit = Math.Abs(balance);
                ba.transactionLog.Add("Withdrawal of " + amount);
                return ($"Your new balance for account {ba.AccountID} is -${ba.Debit}");
            }


        }

        public String Deposit(BusinessCheckingAccount ba, double amount, double balance)
        {
            try
            {
                if (balance >= 0) 
                {
                    ba.Debit = 0;
                    ba.Credit = balance;
                    ba.transactionLog.Add("Deposit of " + amount);
                    return ($"Your new balance for account {ba.AccountID} is ${ba.Credit}");
                }
                else
                {
                    ba.Credit = 0;
                    ba.Debit = -balance;
                    ba.transactionLog.Add("Deposit of " + amount);
                    return ($"Your new balance for account {ba.AccountID} is -${ba.Debit}");
                }
            }
            catch
            {
                throw;
            }
        }

        public Account getAccount(int accountID)
        {
            throw new NotImplementedException();
        }

        public List<Account> getAllAccounts(int accountID)
        {
            throw new NotImplementedException();
        }
    }
}
