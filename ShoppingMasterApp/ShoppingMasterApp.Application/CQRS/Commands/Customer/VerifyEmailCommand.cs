using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class VerifyEmailCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }

        public class Handler : IRequestHandler<VerifyEmailCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (customer == null)
                {
                    throw new ArgumentException("Email bulunamadı.");
                }

                if (customer.VerificationCode != request.VerificationCode)
                {
                    throw new ArgumentException("Doğrulama kodu geçersiz.");
                }

                customer.IsVerified = true;
                _customerRepository.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
