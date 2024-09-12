﻿using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByCustomerIdAsync(int CustomerId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)  
                .AsTracking()  
                .FirstOrDefaultAsync(c => c.CustomerId == CustomerId);  
        }

    }


}
