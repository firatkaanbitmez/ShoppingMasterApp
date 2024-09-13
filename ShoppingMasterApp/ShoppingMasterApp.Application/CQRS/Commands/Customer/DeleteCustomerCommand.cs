using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        // Handler embedded inside the command class
        public class Handler : IRequestHandler<DeleteCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetByIdAsync(request.Id);
                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                _customerRepository.Delete(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
