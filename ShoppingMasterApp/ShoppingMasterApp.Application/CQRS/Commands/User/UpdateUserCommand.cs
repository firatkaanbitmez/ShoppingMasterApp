using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public Address Address { get; set; }

        // Handler embedded inside the command class
        public class Handler : IRequestHandler<UpdateUserCommand, Unit>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = new Email(request.Email);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Roles = (Roles)Enum.Parse(typeof(Roles), request.Roles);
                user.Address = request.Address;

                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
