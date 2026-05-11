using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Enums;

namespace G_Net_40_EF04.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Note { get; set; }

        
        public Account Account { get; set; }
        public int AccountId { get; set; }
    }
}
