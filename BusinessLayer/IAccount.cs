using System;
using Entities;

namespace BusinessLayer
{
    public interface IAccount
    {
        Account Create(Customer cust);
        String Withdraw(int accountID, double amount);
        String Deposit(int accountID, double amount);
        String Transfer(int fromAccount, int toAccount, double amount);
    }
}
