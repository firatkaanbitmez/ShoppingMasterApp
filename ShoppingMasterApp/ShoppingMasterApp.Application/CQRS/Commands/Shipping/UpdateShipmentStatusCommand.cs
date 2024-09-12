using MediatR;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects; // Ensure this using statement is present
namespace ShoppingMasterApp.Application.CQRS.Queries.Shipping
{
    public class UpdateShipmentStatusCommand : IRequest<Unit>
    {
        public int ShippingId { get; set; }
        public ShippingStatus Status { get; set; } // Enum
        public string ShippingCompany { get; set; }
        public decimal ShippingCost { get; set; }
        public Address ShippingAddress { get; set; } // Use Address instead of ShippingAddress

        public class Handler : IRequestHandler<UpdateShipmentStatusCommand, Unit>
        {
            private readonly IShippingRepository _shippingRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IShippingRepository shippingRepository, IUnitOfWork unitOfWork)
            {
                _shippingRepository = shippingRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateShipmentStatusCommand request, CancellationToken cancellationToken)
            {
                var shipping = await _shippingRepository.GetByIdAsync(request.ShippingId);
                if (shipping == null)
                {
                    throw new KeyNotFoundException("Shipping record not found.");
                }

                shipping.ShippingCompany = request.ShippingCompany;
                shipping.ShippingCost = request.ShippingCost;
                shipping.ShippingAddress = request.ShippingAddress; // Using Address here
                shipping.UpdateStatus(request.Status); // Use the enum for status updates

                _shippingRepository.Update(shipping);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }


}
