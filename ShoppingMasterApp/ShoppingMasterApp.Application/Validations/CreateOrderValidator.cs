using FluentValidation;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Validations
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(o => o.ProductId).GreaterThan(0);
            RuleFor(o => o.Quantity).GreaterThan(0);
        }
    }

}
