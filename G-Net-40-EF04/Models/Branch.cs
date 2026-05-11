using System;
using System.Collections.Generic;
using System.Text;

namespace G_Net_40_EF04.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public Manager Manager { get; set; }
        public int ManagerId { get; set; }

        public Account Account { get; set; }
    }
}
