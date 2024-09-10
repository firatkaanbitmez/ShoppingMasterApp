using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteProductCommand,Unit>
        {
            private readonly IProductRepository _productRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IProductRepository productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                _productRepository.Delete(product);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
