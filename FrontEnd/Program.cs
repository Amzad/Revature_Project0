using BusinessLayer;
using System;
using Entities;
using System.Collections.Generic;

namespace FrontEnd
{
    public class Program
    {
        public static void Main(String[] args)
        {
            //generateFakeUser();
            Console.WriteLine("Welcome to the Console Bank!");

            while (true)
            {
                Console.WriteLine("Please select an option below");
                Console.WriteLine("R -  Register");
                Console.WriteLine("O -  Open a new account");
                Console.WriteLine("C -  Close an account");
                Console.WriteLine("W -  Withdraw from an account");
                Console.WriteLine("D -  Deposit to an account");
                Console.WriteLine("T -  Transfer between accounts");
                Console.WriteLine("P -  Pay Loan Installment");
                Console.WriteLine("S -  Display my accounts");
                Console.WriteLine("L -  Display account transactions"); 

                String x = Console.ReadLine().ToUpper(); // Allows for both upper and lowercase inputs

                // Register a new customer
                if (x == "R")
                {
                    try
                    {
                        // Get registration details from customer
                        Console.Clear();
                        Console.WriteLine("Please enter your first name");
                        String firstName = Console.ReadLine();
                        Console.WriteLine("Please enter your last name");
                        String lastName = Console.ReadLine();
                        Console.WriteLine("Please enter your address");
                        String address = Console.ReadLine();
                        Console.WriteLine("Please enter your phone number");
                        String phoneNumber = Console.ReadLine();
                        CustomerBL custBL = new CustomerBL();
                        Customer newCust = custBL.Create(firstName, lastName, address, phoneNumber); //Submit new customer registration


                        Console.Clear();
                        Console.WriteLine($"Your customer account has been created {newCust.FirstName} {newCust.LastName}. Your customer ID number is {newCust.ID}");
                        Console.WriteLine($"Your customer ID number is {newCust.ID}");

                    }
                    catch (NewCustomerException)
                    {
                        Console.WriteLine("Unable to create customer account for unknown reason.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        ResetPrompt();
                    }
                }

                // Create a new account
                else if (x == "O")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter your customer ID");
                        int parsedID = int.Parse(Console.ReadLine());
                        CustomerBL custBL = new CustomerBL();

                        Console.Clear();
                        Customer cust = custBL.Get(parsedID);
                        Console.WriteLine($"Hello {cust.FirstName} {cust.LastName}!");
                        Console.WriteLine("Please select the type of account to open below.");
                        Console.WriteLine("C -  Checking Account");
                        Console.WriteLine("B -  Business Account");
                        Console.WriteLine("L -  Loan");
                        Console.WriteLine("T -  Term Deposit");

                        String input = Console.ReadLine().ToUpper();

                        // Create Checking Account
                        if (input == "C")
                        {
                            Account newAccount = new PersonalCheckingBL().Create(cust);
                            Console.WriteLine($"Your checking account has been created. Your checking account number is {newAccount.AccountID}");
                        }

                        // Create Business Account
                        else if (input == "B")
                        {
                            Account newAccount = new BusinessCheckingBL().Create(cust);
                            Console.WriteLine($"Your business account has been created. Your business account number is {newAccount.AccountID}");
                        }

                        // Create Loan
                        else if (input == "L")
                        {
                            Console.Clear();
                            Console.WriteLine("Enter the amount desired");
                            int amount = int.Parse(Console.ReadLine());
                            LoanAccount newAccount = new LoanBL().Create(cust, amount) as LoanAccount;
                            Console.WriteLine($"Your loan account has been created. Your loan account number is {newAccount.AccountID} and your balance is {newAccount.Debit}.");
                        }

                        // Create term deposit
                        else if (input == "T")
                        {
                            Console.Clear();
                            Console.WriteLine("Enter the term length");
                            String term = Console.ReadLine();
                            Console.WriteLine("Enter the deposit amount");
                            String amount = Console.ReadLine();

                            int termAmount = int.Parse(term);
                            int depositAmount = int.Parse(amount);
                            TermDepositAccount newAccount = new TermDepositBL().Create(cust, depositAmount, termAmount) as TermDepositAccount;

                            Console.Clear();
                            Console.WriteLine($"Your loan account has been created. Your loan account number is {newAccount.AccountID} and your deposit is {newAccount.Credit}");
                            Console.WriteLine($"Your deposit will compound at 3.5% APR for {newAccount.depositTerm} years");
                        }

                        else
                        {
                            Console.WriteLine("Invalid Input");
                        }


                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid entry");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Invalid entry");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        ResetPrompt();
                    }


                }

                // Close an account
                if (x == "C")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter your customer ID.");
                        String id = Console.ReadLine();

                        int custID = int.Parse(id);

                        Dictionary<String, String> accountList = new CustomerBL().GetCustomerAccounts(custID);
                        Console.WriteLine("The following are your available accounts");
                        foreach (KeyValuePair<String, String> s in accountList)
                        {
                            Console.WriteLine(s.Value);
                        }

                        Console.WriteLine("Enter the account ID of the account you would like to close");
                        int accDel = int.Parse(Console.ReadLine());

                        Account account = new CustomerBL().DeleteAccount(custID, accDel);
                        Console.WriteLine($" Your account with the account number {account.AccountID} has been closed");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid account id entered");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Invalid customer id entered.");
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Invalid customer ID entered");
                    }
                    finally
                    {
                        ResetPrompt();
                    }

                }

                // Withdraw from account
                if (x == "W")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter your customer ID");
                        String input = Console.ReadLine();
                        int custID = int.Parse(input);
                        List<Account> custAccList = new CustomerBL().GetAllCustomerAccounts(custID);

                        Console.Clear();

                        // Print all accounts but loan accounts
                        Dictionary<String, String> accountList = new CustomerBL().GetCustomerAccounts(custID);
                        Console.WriteLine("The following are your available accounts");
                        foreach (KeyValuePair<String, String> s in accountList)
                        {
                            if (s.Value != "LoanAccount")
                            {
                                Console.WriteLine(s.Key);
                            }
                        }

                        Console.WriteLine("Please select an account to withdraw from");
                        int selectedID = int.Parse(Console.ReadLine());

                        Console.WriteLine("How much do you want to withdraw?");
                        String amount = Console.ReadLine();
                        double amountValue = double.Parse(amount);


                        Account acc = custAccList.Find(acc2 => acc2.AccountID == selectedID);

                        String returnedString = new AccountBL().Withdraw((BusinessLayer.IAccount)new AccountBL().getType(acc), selectedID, amountValue);

                        Console.Clear();
                        Console.WriteLine(returnedString);
                    }
                    catch (UnavailableFunctionException)
                    {
                        Console.WriteLine("The term deposit isn't accessible right now");
                        //ResetPrompt();
                    }
                    catch (TermLengthException)
                    {
                        Console.WriteLine("The term deposit isn't accessible right now");
                        //ResetPrompt();
                    }
                    catch (OverdraftException)
                    {
                        Console.WriteLine("You have insufficient funds.");
                        //ResetPrompt();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        ResetPrompt();
                    }


                }

                if (x == "D")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter your customer ID");
                        String input = Console.ReadLine();
                        int custID = int.Parse(input);
                        List<Account> custAccList = new CustomerBL().GetAllCustomerAccounts(custID);

                        Console.Clear();

                        // Print all accounts but loan accounts
                        Dictionary<String, String> accountList = new CustomerBL().GetCustomerAccounts(custID);
                        Console.WriteLine("The following are your available accounts");
                        foreach (KeyValuePair<String, String> s in accountList)
                        {
                            if ( (s.Value == "PersonalCheckingAccount") || (s.Value == "BusinessCheckingAccount") )
                            {
                                Console.WriteLine(s.Key);
                            }
                        }

                        Console.WriteLine("Please select an account to deposit to");
                        String selID = Console.ReadLine();
                        int selectedID = int.Parse(selID);

                        Console.WriteLine("How much do you want to deposit?");
                        String amount = Console.ReadLine();
                        double amountValue = double.Parse(amount);

                        Account acc = custAccList.Find(acc2 => acc2.AccountID == selectedID);

                        String returnedString = new AccountBL().Deposit((BusinessLayer.IAccount)new AccountBL().getType(acc), selectedID, amountValue);

                        Console.Clear();
                        Console.WriteLine(returnedString);
                        ResetPrompt();

                    }
                    catch (UnavailableFunctionException) 
                    {
                        Console.WriteLine("This function isn't available for this account");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input");

                    }
                    catch (TermLengthException)
                    {
                        Console.WriteLine("The term deposit has not matured yet.");
                    }
                    catch (OverdraftException)
                    {
                        Console.WriteLine("You have insufficient funds.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }


                }

                if (x == "T")
                {
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("Enter your customer ID");
                        int custID = int.Parse(Console.ReadLine());
                        List<Account> custAccList = new CustomerBL().GetAllCustomerAccounts(custID);

                        // Print all accounts but loan accounts
                        Dictionary<String, String> accountList = new CustomerBL().GetCustomerAccounts(custID);
                        Console.WriteLine("The following are your available accounts");
                        foreach (KeyValuePair<String, String> s in accountList)

                            if (s.Value != "LoanAccount") Console.WriteLine(s.Key);


                        Console.WriteLine("Please select an account to transfer from");
                        String transferFromID = Console.ReadLine();
                        Console.WriteLine("Please select an account to transfer to");
                        String transferToID = Console.ReadLine();
                        int selectedFrom;
                        int selectedTo;


                        selectedFrom = int.Parse(transferFromID);
                        selectedTo = int.Parse(transferToID);
                        if (selectedFrom == selectedTo)
                        {
                            throw new SameAccountException();
                        }

                        Console.WriteLine("How much do you want to transfer?");
                        double amount = double.Parse(Console.ReadLine());

                        Account acc = custAccList.Find(acc2 => acc2.AccountID == selectedFrom);
                        String returnedString = new AccountBL().Transfer((BusinessLayer.IAccount)new AccountBL().getType(acc), selectedFrom, selectedTo, amount);

                        Console.Clear();
                        Console.WriteLine(returnedString);
                        ResetPrompt();


                    }
                    catch (SameAccountException)
                    {
                        Console.WriteLine("You cannot transfer to and from the same account");
                    }
                    catch (TermLengthException)
                    {
                        Console.WriteLine("The term deposit isn't accessible right now");
                    }
                    catch (OverdraftException)
                    {
                        Console.WriteLine("You have insufficient funds.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }

                if (x == "S")
                {
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("Enter your customer ID");
                        String input = Console.ReadLine();
                        int custID = int.Parse(input);
                        Dictionary<String, String> accountList = new CustomerBL().GetCustomerAccounts(custID);
                        if (accountList.Count > 0)
                        {
                            Console.WriteLine("The following are your available accounts");
                            foreach (KeyValuePair<String, String> s in accountList)
                                Console.WriteLine(s.Key);

                        }
                        else
                        {
                            Console.WriteLine("No accounts exist for your customer id");
                        }
                        ResetPrompt();
                    }
                    catch
                    {
                        Console.Clear();
                    }

                }

                if (x == "P")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Enter your customer ID");
                        String input = Console.ReadLine();
                        int custID = int.Parse(input);
                        List<Account> custAccList = new CustomerBL().GetAllCustomerAccounts(custID);
                        Console.Clear();
                        List<Account> loanAccounts = custAccList.FindAll(acc => acc is LoanAccount);
                        if (loanAccounts.Count > 0)
                        {

                            Console.WriteLine("The following are your available loan accounts");
                            foreach (LoanAccount loanAcc in loanAccounts)
                            {
                                Console.WriteLine($"Type: Loan  Debit: {loanAcc.Debit}  Interest Rate: {loanAcc.interestRate} ID: {loanAcc.AccountID}");
                            }

                            Console.WriteLine("Please select an account id to pay");
                            String selectAcc = Console.ReadLine();
                            int selectedAcc = int.Parse(selectAcc);

                            Console.WriteLine("How much do you want to pay?");
                            String selectAmount = Console.ReadLine();
                            int selectedAmount = int.Parse(selectAmount);

                            String returnedString = new LoanBL().PayInstallment(selectedAcc, selectedAmount);
                            Console.WriteLine(returnedString);
                        }
                        else
                        {
                            Console.WriteLine("No loan accounts are available for your account");
                        }
                    }
                    catch (UnavailableFunctionException)
                    {
                        Console.WriteLine("The selected function isn't available");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        ResetPrompt();
                    }
                }
                if (x == "L")
                {
                    try
                    {
                        Console.Clear();

                        Console.WriteLine("Enter your customer ID");
                        String input = Console.ReadLine();
                        int custID = int.Parse(input);
                        Console.WriteLine("The following are your available accounts");

                        // Print all accounts
                        Dictionary<String, String> accountList = new CustomerBL().GetCustomerAccounts(custID);
                        Console.WriteLine("The following are your available accounts");
                        foreach (KeyValuePair<String, String> s in accountList)
                            Console.WriteLine(s.Key);


                        Console.WriteLine("Please select an account to view the transaction logs");
                        int transAcc = int.Parse(Console.ReadLine());

                        List<String> transLog = new AccountBL().getTransactionLog(transAcc);
                        if (transLog.Count > 0)
                        {
                            foreach (String s in transLog) Console.WriteLine(s);
                        }
                        else
                        {
                            Console.WriteLine("No transaction records exists for this account");
                        }
                        ResetPrompt();
                    }
                    catch
                    {
                        Console.WriteLine("Unhandled Error");

                    }


                }
            }
        }

        static void ResetPrompt()
        {
            Console.WriteLine("Press the enter key to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        static void generateFakeUser()
        {
            new CustomerBL().Create("Amzad", "Chowdhury", "100 Washington Ave", "646-100-2000");
        }
    }
}
