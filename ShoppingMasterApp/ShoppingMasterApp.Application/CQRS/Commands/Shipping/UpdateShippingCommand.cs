using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;
namespace ShoppingMasterApp.Application.CQRS.Queries.Shipping
{
    public class UpdateShippingCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public ShippingStatus NewStatus { get; set; }

        public class Handler : IRequestHandler<UpdateShippingCommand, Unit>
        {
            private readonly IShippingRepository _shippingRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IShippingRepository shippingRepository, IUnitOfWork unitOfWork)
            {
                _shippingRepository = shippingRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateShippingCommand request, CancellationToken cancellationToken)
            {
                var shipping = await _shippingRepository.GetShippingByOrderIdAsync(request.OrderId);
                if (shipping == null)
                {
                    throw new KeyNotFoundException("Shipping record not found");
                }

                shipping.UpdateStatus(request.NewStatus);

                _shippingRepository.Update(shipping);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }


}
