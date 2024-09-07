using ShoppingMasterApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Payment
{
    public class ProcessPaymentCommand
    {
        public int OrderId { get; set; }
        public string CardType { get; set; }  // Add this
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Amount { get; set; }  // Add this
        public string Cvv { get; set; }
    }

}
