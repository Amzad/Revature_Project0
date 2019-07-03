using System;
using Entities;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class AccountBL
    {
        IAccount _account;

        public String Withdraw(IAccount account, int accountID, double amount)
        {
            return account.Withdraw(accountID, amount);
        }

        public String Deposit(IAccount account, int accountID, double amount)
        {
            return account.Deposit(accountID, amount);
        }

        public String Transfer(IAccount account, int fromAccountID, int toAccountID, double amount)
        {
            return account.Transfer(fromAccountID, toAccountID, amount);
        }

        public Account GetAccount(int custID)
        {
            try
            {
                return new AccountDAL().GetAccount(custID);

            }
            catch
            {
                throw;
            }
        }

        public List<Account> GetCustomerAccountList(int custID)
        {
            try
            {
                return new AccountDAL().GetAllCustAccounts(custID);

            } catch
            {
                throw;
            }
        }

        public Account deleteCustomerAccount(int accID)
        {
            return new AccountDAL().deleteCustomerAccount(accID);
        }

        public object getType(Account acc)
        {
            if (acc is PersonalCheckingAccount)
            {
                return new PersonalCheckingBL();
            }
            else
            if (acc is BusinessCheckingAccount)
            {
                return new BusinessCheckingBL();
            }
            else
            if (acc is TermDepositAccount)
            {
                return new TermDepositBL();
            } else
            if (acc is LoanAccount)
            {
                return new LoanBL();
            }
            else
            {
                Console.WriteLine("Withdrawals is not available for the account entered.");
                return null;
            }
        }
    }
}
