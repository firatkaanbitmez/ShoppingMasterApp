using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using ShoppingMasterApp.Infrastructure.Repositories;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Categories { get; }
        public IOrderRepository Orders { get; }
        public IProductRepository Products { get; }
        public ICartRepository Carts { get; }
        public IShippingRepository Shippings { get; }
        public IReviewRepository Reviews { get; }
        public IPaymentRepository Payments { get; }

        public UnitOfWork(ApplicationDbContext context, ICategoryRepository categoryRepository,
                          IOrderRepository orderRepository, IProductRepository productRepository,
                          ICartRepository cartRepository, IShippingRepository shippingRepository,
                          IReviewRepository reviewRepository,
                          IPaymentRepository paymentRepository)
        {
            _context = context;
            Categories = categoryRepository;
            Orders = orderRepository;
            Products = productRepository;
            Carts = cartRepository;
            Shippings = shippingRepository;
            Reviews = reviewRepository;
            Payments = paymentRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
