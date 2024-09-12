using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        // Handler embedded inside the command class
        public class Handler : IRequestHandler<DeleteCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository userRepository, IUnitOfWork unitOfWork)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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
