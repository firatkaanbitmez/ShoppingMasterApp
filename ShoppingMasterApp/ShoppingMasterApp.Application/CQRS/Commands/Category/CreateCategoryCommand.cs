using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Category
{
    public class CreateCategoryCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateCategoryCommand,Unit>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            {
                _categoryRepository = categoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = new ShoppingMasterApp.Domain.Entities.Category
                {
                    Name = request.Name,
                    Description = request.Description
                };

                await _categoryRepository.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
