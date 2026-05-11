using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Enums;

namespace G_Net_40_EF04.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateOnly OpeningDate { get; set; }
        public AccountType AccountType { get; set; }

        public Branch Branch { get; set; }
        public int BranchId { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new HashSet<CustomerAccount>();
        public Transaction Transaction { get; set; }

    }
}
