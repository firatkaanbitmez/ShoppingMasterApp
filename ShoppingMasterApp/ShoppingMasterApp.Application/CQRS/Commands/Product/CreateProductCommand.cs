using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class CreateProductCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public string Manufacturer { get; set; }
        public string Sku { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand, Unit>
        {
            private readonly IProductRepository _productRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IProductRepository productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var productDetails = new ProductDetails(request.Manufacturer, request.Sku);
                var product = new ShoppingMasterApp.Domain.Entities.Product(
                    request.Name,
                    request.Description,
                    new Money(request.Price, "USD"),
                    request.Stock,
                    request.CategoryId,
                    productDetails);

                await _productRepository.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }


    }
}
