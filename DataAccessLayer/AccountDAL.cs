using System.Collections.Generic;
using Entities;
namespace DataAccessLayer
{
    public class AccountDAL
    {
        public static List<Account> accountList = new List<Account>();

        public Account GetAccount(int accountID)
        {
            return accountList.Find(acc => acc.AccountID == accountID);
        }

        public List<Account> GetAllCustAccounts(int custID)
        {
            List<Account> customerAccountList = accountList.FindAll(acc => acc.customerID == custID);

            return customerAccountList;
        }

        public Account deleteCustomerAccount(int accID)
        {
            Account acc = accountList.Find(acc2 => acc2.AccountID == accID);
            accountList.Remove(acc);
            return acc;
        }
    }
}
