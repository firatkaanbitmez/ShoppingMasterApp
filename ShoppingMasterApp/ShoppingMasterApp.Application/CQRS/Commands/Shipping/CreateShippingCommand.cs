using MediatR;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
namespace ShoppingMasterApp.Application.CQRS.Queries.Shipping
{
    public class CreateShippingCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public string ShippingCompany { get; set; }
        public decimal ShippingCost { get; set; }
        public Address ShippingAddress { get; set; } // Use Address here

        public class Handler : IRequestHandler<CreateShippingCommand, Unit>
        {
            private readonly IShippingRepository _shippingRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IShippingRepository shippingRepository, IUnitOfWork unitOfWork)
            {
                _shippingRepository = shippingRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
            {
                var shipping = new ShoppingMasterApp.Domain.Entities.Shipping
                {
                    OrderId = request.OrderId,
                    ShippingCompany = request.ShippingCompany,
                    ShippingCost = request.ShippingCost,
                    ShippingAddress = request.ShippingAddress, // Address instead of ShippingAddress
                    Status = ShippingStatus.Preparing
                };

                await _shippingRepository.AddAsync(shipping);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }

}
