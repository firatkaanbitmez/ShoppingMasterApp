using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Category
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteCategoryCommand,Unit>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            {
                _categoryRepository = categoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(request.Id);
                if (category == null)
                {
                    throw new KeyNotFoundException("Category not found");
                }

                _categoryRepository.Delete(category);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
