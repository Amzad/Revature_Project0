using System;
using Entities;
using DataAccessLayer;
namespace BusinessLayer
{
    public class BusinessCheckingBL : IAccount
    {
        public Account Create(Customer cust)
        {
            try
            {
                Account newAccount = new BusinessCheckingDAL().Create(cust);
                newAccount = new CustomerDAL().addAccount(cust, newAccount);
                return newAccount;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public String Withdraw(int accountID, double amount)
        {
            try
            {

                BusinessCheckingAccount ba = AccountDAL.accountList.Find(account => account.AccountID == accountID) as BusinessCheckingAccount;
                if (ba.Credit > 0)
                {
                    double setAmount = ba.Credit - amount;
                    if (setAmount < 0)
                    {
                        return new BusinessCheckingDAL().Withdraw(ba, amount, setAmount); // return negative value for debit
                    } else
                    {
                        return new BusinessCheckingDAL().Withdraw(ba, amount, setAmount); // return positive value for credit
                    }
                }
                else if (ba.Debit > 0)
                {
                    double remainder = ba.Debit + amount;
                    return new BusinessCheckingDAL().Withdraw(ba, amount, -remainder);
                }
                else
                {
                    return new BusinessCheckingDAL().Withdraw(ba, amount, -amount); 
                }
            }
            catch
            {
                throw;
            }
        }

        public String Deposit(int accountID, double amount)
        {
            try
            {
                BusinessCheckingAccount ba = AccountDAL.accountList.Find(account => account.AccountID == accountID) as BusinessCheckingAccount;
                if (ba.Credit > 0)
                {
                    double setAmount = ba.Credit + amount;
                    return new BusinessCheckingDAL().Deposit(ba, amount, setAmount);
                }
                else if (ba.Debit > 0)
                {  
                    double remainder = ba.Debit - amount; // -4700 = 300 - 5000
                    if (remainder <= 0)
                    {

                        return new BusinessCheckingDAL().Deposit(ba, amount,  Math.Abs(remainder));
                    }
                    else
                    {
                        return new BusinessCheckingDAL().Deposit(ba, amount, -remainder);
                    }
                }
                else
                {
                    return new BusinessCheckingDAL().Deposit(ba, amount, amount);
                }
            }
            catch
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
