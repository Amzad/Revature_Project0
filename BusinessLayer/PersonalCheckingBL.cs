using System;
using Entities;
using DataAccessLayer;
namespace BusinessLayer
{
    public class PersonalCheckingBL : IAccount
    {
        public Account Create(Customer cust)
        {
            try
            {
                Account newAccount = new PersonalCheckingDAL().Create(cust);
                newAccount = new CustomerDAL().addAccount(cust, newAccount);
                return newAccount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public String Withdraw(int accountID, double amount)
        {
            try
            {
                PersonalCheckingAccount pa = AccountDAL.accountList.Find(account => account.AccountID == accountID) as PersonalCheckingAccount;
                double balance = pa.Credit - amount;
                if (balance >= 0)
                {
                    return new PersonalCheckingDAL().Withdraw(pa, amount, balance);
                }
                else
                {
                    throw new OverdraftException();
                }

            }
            catch (OverdraftException)
            {
                throw;
            }
        }

        public String Deposit(int accountID, double amount)
        {
            try
            {
                PersonalCheckingAccount pa = AccountDAL.accountList.Find(account => account.AccountID == accountID) as PersonalCheckingAccount;
                double balance = pa.Credit + amount;
                return new PersonalCheckingDAL().Deposit(pa, amount, balance);

            }
            catch (OverdraftException)
            {
                throw;
            }
        }

        public String Transfer(int fromAccount, int toAccount, double amount)
        {
            try
            {
                AccountBL azBL = new AccountBL();
                var fromAcc = new AccountBL().GetAccount(fromAccount);
                var toAcc = new AccountBL().GetAccount(toAccount);
                if ((toAcc is PersonalCheckingAccount) || (toAcc is BusinessCheckingAccount))
                {
                    Withdraw(fromAccount, amount);
                    new AccountBL().Deposit((BusinessLayer.IAccount)new AccountBL().getType(toAcc), toAccount, amount);
                    return ($"Your transfer of ${amount} was completed from account {fromAccount} to account {toAccount}");
                }
                throw new UnavailableFunctionException();

            }
            catch
            {
                throw;
            }
        }
    }
}
