using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Enums;

namespace G_Net_40_EF04.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfBirth { get; set; } 
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }

        public CustomerType Type { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new HashSet<CustomerAccount>();

    }
}
