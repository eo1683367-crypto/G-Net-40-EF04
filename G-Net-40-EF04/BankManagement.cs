using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Data;
using G_Net_40_EF04.Enums;
using G_Net_40_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace G_Net_40_EF04
{
    public static class BankManagement
    {

        public static void AddCustomer(BankDbContext bankDbContext)
        {
            Console.WriteLine("--- Add New Customer ---");

            // Receive Data From User: 
            string fullName = Prompt("Full Name     ");
            string nationalId = Prompt("National ID       ");

            DateOnly dateOfBirth;
            while (!DateOnly.TryParse(Prompt("Date of Birth      (yyyy-MM-dd) "), out dateOfBirth))
            {
                DisplayError("Invaild Data Format. Please enter in YYYY-MM-DD Format. ");
            }

            string email = Prompt("Email      ");
            string phone = Prompt("Phone      ");
            string address = Prompt("Address      ");

            Console.WriteLine("Customer Type: \n      1) Individual\n      2) Business ");
            CustomerType customerType = Prompt("Choice  ")
            switch
            {
                "1" => CustomerType.Individual,
                "2" => CustomerType.Business,
                _ => CustomerType.Individual
            };
            
            // Check for Unique National Id:
            if(bankDbContext.Customers.Any(c => c.NationalId == nationalId))
            {
                DisplayError("A customer with the same national Id aready exist.");
                return;
            }


            // else: create new customer.
            Customer customer01 = new()
            {
                FullName = fullName,
                NationalId = nationalId,
                DateOfBirth = dateOfBirth,
                Email = email,
                PhoneNumber = phone,
                Address = address,
                Type = customerType,
            };

            // CREATE , ADD : new customer in dbcontext
            bankDbContext.Customers.Add(customer01);
            // Save Changes in DB
            bankDbContext.SaveChanges();
            // Display Success Message
            DisplaySucceess($"Customer Created Successfully. Customer Id = {customer01.Id}");

        }

        public static void OpenAccountForCustomer(BankDbContext bankDbContext)
        {
            Console.WriteLine("--- Open New Account ---");

            // Receive Data From User

            int accNumber = default;
            var accountNumber = Prompt("Account Number   ");
            while (!int.TryParse(accountNumber , out  accNumber))
            {
                DisplayError("Account Number Must be Vaild Number !");
            }

            Console.WriteLine("Account Type: \n      1) Savings\n      2) Current\n      3) Business");
            AccountType accountType = Prompt("Choice  ")
           switch
            {
                "1" => AccountType.Savings,
                "2" => AccountType.Current,
                "3" => AccountType.Business,
                _ => AccountType.Savings
            };

            var branchCode = Prompt("Branch Code      ");
            var customerId = Prompt("Customer Id      ");

            int CusId = default;


            while (!int.TryParse(customerId, out CusId))
            {
                DisplayError("Customer ID Must be Vaild ID !");
            }


            Console.WriteLine("Ownership Role   :\n      1) Primary\n      2) CoHolder");
            OwnershipType ownershipType = Prompt("Choice  ")
           switch
            {
                "1" => OwnershipType.Primary,
                "2" => OwnershipType.CoHolder,
                _ => OwnershipType.Primary
            };


            // Checks on BranchCode and CustomerId
            if(!bankDbContext.Branches.Any(b => b.Code.ToLower() == branchCode.ToLower()))
            {
                DisplayError($"The Branch With Code {branchCode} does Not Exist !");
                return;
            }
            //---------------------------------------------------------------------------------
            if (!bankDbContext.Customers.Any(c => c.Id.ToString() == customerId.ToString()))
            {
                DisplayError($"The Customer With Id = {customerId} does Not Exist !");
                return;
            }
            //---------------------------------------------------------------------------------

            // Get Branch Id 
            var branchId = bankDbContext.Branches.FirstOrDefault(b => b.Code == branchCode).Id;

            // Craete New Account

            Account account01 = new()
            {
                AccountNumber = accNumber,
                CurrentBalance = 0 ,
                BranchId = branchId,
                OpeningDate = DateOnly.FromDateTime(DateTime.Now),
                AccountType = accountType

            };

            // Add Account
            bankDbContext.Accounts.Add(account01);
            // Save Changes In DB
            bankDbContext.SaveChanges();

            //---------------------------------------------------------------------------

            // Get Account Id 
            var accId = bankDbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accNumber).Id;

            // Link Customer to Account

            CustomerAccount customerAccount = new()
            {
                CustomerId = CusId,
                OwnershipStartDate = DateOnly.FromDateTime(DateTime.Now),
                ownershipType = ownershipType,
                AccountStatus = AccountStatus.Active,
                AccountId = accId
            };

            // Add Customer_Account
            bankDbContext.CustomerAccounts.Add(customerAccount);
            // Save Changes In DB
            bankDbContext.SaveChanges();
            // Display Success Message
            DisplaySucceess($"Validating branch '{branchCode}' and customer #{CusId}...");
            DisplaySucceess($"Account '{accNumber}' created and linked to customer {CusId} as {ownershipType} owner.");
         
        }

        public static void UpdateAccountStatus(BankDbContext bankDbContext)
        {
            Console.WriteLine("--- Update Account Status ---");

         
            int accNumber = default;
            string accountNumber;
            do
            {
                accountNumber = Prompt("Account Number   ");
            } while (!int.TryParse(accountNumber, out accNumber));
      

            
            int cusId = default;
            string customerId;
            do
            {
                customerId = Prompt("Customer Id      ");
            } while (!int.TryParse(customerId, out cusId));
            

         
            Console.WriteLine("New Status : \n      1) Active\n      2) Closed");
            AccountStatus accountStatus = Prompt("Choice  ") switch
            {
                "1" => AccountStatus.Active,
                "2" => AccountStatus.Closed,
                _ => AccountStatus.Active
            };

            // get customer account link to update status 
            var customerAccount = bankDbContext.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.CustomerId == cusId &&
                    ca.Account.AccountNumber == accNumber);
            

            if (customerAccount is null)
            {
                DisplayError($"No link found between Customer {cusId} and Account {accNumber}.");
                return;
            }

           
            customerAccount.AccountStatus = accountStatus;
            bankDbContext.SaveChanges();

            DisplaySucceess($"Status updated to {accountStatus}.");
        }

        public static void RemoveAccountFromCustomer(BankDbContext bankDbContext)
        {
            Console.WriteLine("--- Remove Account from Customer ---");

            int accNumber = default;
            string accountNumber;
            do
            {
                accountNumber = Prompt("Account Number   ");
            } while (!int.TryParse(accountNumber, out accNumber));



            int cusId = default;
            string customerId;
            do
            {
                customerId = Prompt("Customer Id      ");
            } while (!int.TryParse(customerId, out cusId));


            // get customer account link to update status 
            var customerAccount = bankDbContext.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.CustomerId == cusId &&
                    ca.Account.AccountNumber == accNumber);


            if (customerAccount is null)
            {
                DisplayError($"No link found between Customer {cusId} and Account {accNumber}.");
                return;
            }

            bankDbContext.CustomerAccounts.Remove(customerAccount);
            bankDbContext.SaveChanges();

            DisplaySucceess("Ownership Link deleted.");

            // if there is any account linked to the same account number, we should not delete the account, otherwise we can delete it.

            var hasOtherOwner = bankDbContext.CustomerAccounts.Any(ca => ca.Account.AccountNumber == accNumber);

            if (!hasOtherOwner)
            {
                var account = bankDbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accNumber);
                if (account != null)
                {
                    bankDbContext.Accounts.Remove(account);
                    bankDbContext.SaveChanges();
                }
            }

        }

        public static void ListAllCustomers(BankDbContext bankDbContext)
        {
            Console.WriteLine("--- All Customers ---\n");

            var customers = bankDbContext.Customers
                .Include(c => c.CustomerAccounts)
                    .ThenInclude(ca => ca.Account)
                        .ThenInclude(a => a.Branch)
                .ToList();

            if (!customers.Any())
            {
                Console.WriteLine("No customers found.");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine($"  #{customer.Id} {customer.FullName} ({customer.Type})");

                if (!customer.CustomerAccounts.Any())
                {
                    Console.WriteLine("      (no accounts)");
                }
                else
                {
                    foreach (var ca in customer.CustomerAccounts)
                    {
                        var acc = ca.Account;
                        Console.WriteLine(
                            $"      {acc.AccountNumber,-10} {acc.AccountType,-10} " +
                            $"Balance: {acc.CurrentBalance,12:F2}   " +
                            $"{ca.ownershipType,-10} {ca.AccountStatus,-10} " +
                            $"@ {acc.Branch?.Name ?? "Unknown Branch"}");
                    }
                }
            }
        }


        #region Statement Print
        private static string Prompt(string label)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{label}:  ");
            Console.ResetColor();
            return Console.ReadLine() ?? string.Empty;
        }
        //--------------------------------------------------
        private static void DisplaySucceess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        //--------------------------------------------------
        private static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        //-------------------------------------------------- 
        #endregion

    }
}
