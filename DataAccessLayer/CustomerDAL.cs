using System;
using System.Collections.Generic;
using Entities;
namespace DataAccessLayer
{
	public class CustomerDAL
	{

        public static List<Customer> customerList = new List<Customer>();

		public Customer Create(Customer cust)
		{
            try
            {
                Customer newCust = cust;
                newCust.ID = (customerList.Count + 1);
                Console.WriteLine("Customer created " + newCust.FirstName);
                customerList.Add(newCust);
                return newCust;
            } catch (NewCustomerException)
            {
                throw;
            }
		}

        public Account addAccount(Customer cust, Account account)
        {
            cust.Accounts.Add(account.AccountID);

            return account;
        }

		public Customer Get(int custID)
		{
            try
            {
                Customer cust = customerList.Find(c => c.ID == custID);
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

        public int deleteAccount(int custID, int accID)
        {
            foreach(Customer customer in customerList)
            {
                if (customer.Accounts.Remove(accID)) return accID;
            }

            return 0;
        }
	}
}
