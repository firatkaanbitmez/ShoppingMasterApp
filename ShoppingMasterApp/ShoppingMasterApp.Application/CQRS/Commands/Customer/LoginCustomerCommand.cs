using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using ShoppingMasterApp.Application.Services;

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
            private readonly ITokenService _tokenService;

            public Handler(ICustomerRepository customerRepository, ITokenService tokenService)
            {
                _customerRepository = customerRepository;
                _tokenService = tokenService;
            }

            public async Task<string> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (customer == null || !BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }
                            

                // Eğer e-posta doğrulaması gerekiyorsa
                if (!customer.IsEmailVerified)
                {
                    throw new UnauthorizedAccessException("User email is not verified. Please verify your email.");
                }

                //Eğer SMS doğrulaması gerekiyorsa
                 if (!customer.IsSmsVerified)
                {
                    throw new UnauthorizedAccessException("User SMS is not verified. Please verify your phone number.");
                }

                // JWT token oluştur
                var token = _tokenService.GenerateToken(customer);
                return token;
            }

        }
    }


}
