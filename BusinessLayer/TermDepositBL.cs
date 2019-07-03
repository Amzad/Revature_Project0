using System;
using Entities;
using DataAccessLayer;
namespace BusinessLayer
{
    public class TermDepositBL : IAccount
    {

        public Account Create(Customer cust, int amount, int length)
        {
            try
            {
                Account newAccount = new TermDepositDAL().Create(cust, amount, length);
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

        // Not used. Throw unimplementederror.
        public string Deposit(int accountID, double amount)
        {
            throw new UnavailableFunctionException();
        }

        public String Withdraw(int accountID, double amount)
        {
            try
            {
                TermDepositAccount ta = AccountDAL.accountList.Find(account => account.AccountID == accountID) as TermDepositAccount;

                if (ta.depositTerm == 0)
                {
                    ta.Credit -= amount;
                    ta.transactionLog.Add("Withdrawal of " + amount);
                    return new TermDepositDAL().Withdraw(ta, amount);
                }
                else
                {
                    throw new TermLengthException();
                }
            }
            catch (TermLengthException)
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
