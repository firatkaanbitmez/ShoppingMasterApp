using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;
namespace ShoppingMasterApp.Application.CQRS.Commands.Payment
{

    public class ProcessPaymentCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }

      
    }

}
