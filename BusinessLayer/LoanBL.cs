using System;
using Entities;
using DataAccessLayer;
namespace BusinessLayer
{
    public class LoanBL : IAccount
    {
        public Account Create(Customer cust, int amount)
        {
            try
            {
                Account newAccount = new LoanDAL().Create(cust, amount);
                newAccount = new CustomerDAL().addAccount(cust, newAccount);
                return newAccount;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public Account Create(Customer cust)
        {
            throw new NotImplementedException();
        }

        public string Deposit(int accountID, double amount)
        {
            throw new UnavailableFunctionException();
        }

        public String PayInstallment(int accountID, double amount)
        {
            LoanAccount la = AccountDAL.accountList.Find(acc => acc.AccountID == accountID) as LoanAccount;
            return new LoanDAL().PayInstallment(la, amount);

        }

        public string Withdraw(int accountID, double amount)
        {
            throw new UnavailableFunctionException();

        }

        public String Transfer(int fromAccount, int toAccount, double amount)
        {

            throw new UnavailableFunctionException();
        }
    }
}
