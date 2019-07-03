using System;
using System.Collections.Generic;
using DataAccessLayer;
using Entities;


namespace BusinessLayer
{
    public class CustomerBL
    {

        public Customer Create(String firstName, String lastName, String custAddress, String phoneNumber)
        {
            try
            {
                CustomerDAL custDal = new CustomerDAL();
                Customer newCust = custDal.Create(new Customer() { FirstName = firstName, LastName = lastName, Address = custAddress, PhoneNumber = phoneNumber });

                return newCust;
            }
            catch (NewCustomerException)
            {
                throw;
            }
        }

        public Customer Get(int custID)
        {
            try
            {
                Customer cust = new CustomerDAL().Get(custID);

                return cust;
            }
            catch (CustomerNotFoundException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public Account DeleteAccount(int custID, int accID)
        {
            new CustomerDAL().deleteAccount(custID, accID);
            return new AccountDAL().deleteCustomerAccount(accID);
        }

        public List<Account> GetAllCustomerAccounts(int custID)
        {
            return new AccountDAL().GetAllCustAccounts(custID);
        }

        public List<String> GetAccountList(int custID)
        {
            List<String> accountList = new List<String>();
            List<Account> custAccList = GetAllCustomerAccounts(custID);
            Console.Clear();
            Console.WriteLine("The following are your available accounts");
            foreach (Account acc in custAccList)
            {
                if (acc is PersonalCheckingAccount)
                {
                    PersonalCheckingAccount acc2 = acc as PersonalCheckingAccount;
                    accountList.Add($"Type: Personal Checking  Credit: {acc2.Credit}  Debit: {acc2.Debit}  Interest Rate: {acc2.interestRate}  ID: {acc.AccountID}");
                }
                if (acc is BusinessCheckingAccount)
                {
                    BusinessCheckingAccount acc2 = acc as BusinessCheckingAccount;
                    accountList.Add($"Type: Business Checking  Credit: {acc2.Credit}  Debit: {acc2.Debit}  Interest Rate: {acc2.interestRate}  ID: {acc.AccountID}");
                }
                if (acc is LoanAccount)
                {
                    LoanAccount acc2 = acc as LoanAccount;
                    accountList.Add($"Type: Loan  Debit: {acc2.Debit}  Interest Rate: {acc2.interestRate} ID: {acc.AccountID}");
                }
                if (acc is TermDepositAccount)
                {
                    TermDepositAccount acc2 = acc as TermDepositAccount;
                    accountList.Add($"Type: Term Deposit  Debit: {acc2.depositTerm}  Interest Rate: {acc2.interestRate} ID: {acc.AccountID}");
                }

            }
            return accountList;
        }

        public Dictionary<String, String> GetCustomerAccounts(int custID)
        {
            Dictionary<String, String> accountLists = new Dictionary<string, string>();
            List<Account> custAccList = GetAllCustomerAccounts(custID);

            Console.Clear();
            //Console.WriteLine("The following are your available accounts");

            foreach (Account acc in custAccList)
            {
                if (acc is PersonalCheckingAccount)
                {
                    PersonalCheckingAccount acc2 = acc as PersonalCheckingAccount;
                    accountLists.Add($"Type: Personal Checking  Credit: {acc2.Credit}  Debit: {acc2.Debit}  ID: {acc.AccountID}", "PersonalCheckingAccount");
                }
                else if (acc is BusinessCheckingAccount)
                {
                    BusinessCheckingAccount acc2 = acc as BusinessCheckingAccount;
                    accountLists.Add($"Type: Business Checking  Credit: {acc2.Credit}  Debit: {acc2.Debit}  ID: {acc.AccountID}", "BusinessCheckingAccount");
                }
                else if (acc is LoanAccount)
                {
                    LoanAccount acc2 = acc as LoanAccount;
                    accountLists.Add($"Type: Loan  Debit: {acc2.Debit}  Interest Rate: {acc2.interestRate} ID: {acc.AccountID}", "LoanAccount");
                }
                else if (acc is TermDepositAccount)
                {
                    TermDepositAccount acc2 = acc as TermDepositAccount;
                    accountLists.Add($"Type: Term Deposit  Debit: {acc2.depositTerm}  Interest Rate: {acc2.interestRate} ID: {acc.AccountID}", "TermDepositAccount");
                }
            }

            return accountLists;
        }
    }
}
