
using G_Net_40_EF04;
using G_Net_40_EF04.Data;

using var bankDbContext = new BankDbContext();

DatabaseSeeding.Seed(bankDbContext);

while (true)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("      National Bank — Management        ");
    Console.WriteLine("========================================");
    Console.WriteLine("  1) Add a new Customer");
    Console.WriteLine("  2) Open a new Account for a Customer");
    Console.WriteLine("  3) Update Account Status (Active / Closed)");
    Console.WriteLine("  4) Remove an Account from a Customer");
    Console.WriteLine("  5) List all Customers (with accounts)");
    Console.WriteLine("  0) Exit");
    Console.WriteLine("----------------------------------------");
    Console.Write("  Enter choice: ");

    var choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            BankManagement.AddCustomer(bankDbContext);
            break;
        case "2":
            BankManagement.OpenAccountForCustomer(bankDbContext);
            break;
        case "3":
            BankManagement.UpdateAccountStatus(bankDbContext);
            break;
        case "4":
            BankManagement.RemoveAccountFromCustomer(bankDbContext);
            break;
        case "5":
            BankManagement.ListAllCustomers(bankDbContext);
            break;
        case "0":
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("  Invalid choice. Please enter a number from 0 to 5.");
            break;
    }

    Console.WriteLine();
    Console.Write("Press any key to return to the menu...");
    Console.ReadKey();
}