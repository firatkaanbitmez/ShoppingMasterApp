using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Admin
{
    public class LoginAdminCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginAdminCommand, string>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;

            public Handler(IAdminRepository adminRepository, IJwtTokenGenerator jwtTokenGenerator)
            {
                _adminRepository = adminRepository;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<string> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
            {
                var admin = await _adminRepository.GetAdminByEmailAsync(request.Email);
                if (admin == null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Invalid credentials.");
                }

                return _jwtTokenGenerator.GenerateToken(admin);
            }
        }
    }
}
