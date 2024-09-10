using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        // Handler embedded inside the command class
        public class Handler : IRequestHandler<DeleteUserCommand, Unit>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found");
                }

                _userRepository.Delete(user);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
