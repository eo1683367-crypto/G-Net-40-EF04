using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Models;
using Microsoft.EntityFrameworkCore;

namespace G_Net_40_EF04.Data
{
    public class BankDbContext : DbContext
    {
        // Configure the database connection string and other options here
        #region Configure Connection String
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // configure the connection string to your database
            optionsBuilder.UseSqlServer("Server= DESKTOP-65SBLOR; Database= NationalBankDB; trusted_connection= True; trustservercertificate= True;");
        }
        #endregion
        // Define your DbSet properties for each entity here
        #region My DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }



        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // السطر الواحد ده بيلاقي كل الـ Configuration Classes 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankDbContext).Assembly);

        }
    }
}
