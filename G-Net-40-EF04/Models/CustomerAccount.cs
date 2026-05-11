using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Enums;

namespace G_Net_40_EF04.Models
{
    public class CustomerAccount
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }


        public OwnershipType ownershipType { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public DateOnly OwnershipStartDate { get; set; }

    }
}
