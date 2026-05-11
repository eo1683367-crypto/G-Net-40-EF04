using System;
using System.Collections.Generic;
using System.Text;

namespace G_Net_40_EF04.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly HireDate { get; set; }

        public Branch Branch { get; set; }
    }
}
