using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class LoginCustomerCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginCustomerCommand, string>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;

            public Handler(ICustomerRepository customerRepository, IJwtTokenGenerator jwtTokenGenerator)
            {
                _customerRepository = customerRepository;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<string> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (customer == null || !BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }

                // Generate JWT token
                var token = _jwtTokenGenerator.GenerateToken(customer);
                return token;
            }
        }
    }


}
