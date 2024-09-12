using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
namespace ShoppingMasterApp.Application.CQRS.Queries.Shipping
{
    public class CalculateShippingCostCommand : IRequest<decimal>
    {
        public int OrderId { get; set; }
        public string ShippingAddress { get; set; }

        public class Handler : IRequestHandler<CalculateShippingCostCommand, decimal>
        {
            private readonly IShippingRepository _shippingRepository;

            public Handler(IShippingRepository shippingRepository)
            {
                _shippingRepository = shippingRepository;
            }

            public async Task<decimal> Handle(CalculateShippingCostCommand request, CancellationToken cancellationToken)
            {
                // Logic to calculate shipping cost
                decimal baseCost = 10.0m; // Assume a base shipping cost
                decimal distanceFactor = CalculateDistanceFactor(request.ShippingAddress); // Example

                decimal totalCost = baseCost * distanceFactor;
                return await Task.FromResult(totalCost);
            }

            private decimal CalculateDistanceFactor(string shippingAddress)
            {
                // Logic to calculate distance factor based on address (dummy logic here)
                return 1.5m; // Placeholder for actual distance calculation
            }
        }
    }
}