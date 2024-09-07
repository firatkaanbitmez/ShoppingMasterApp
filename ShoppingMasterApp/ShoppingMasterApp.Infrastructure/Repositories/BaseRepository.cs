using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        
    }
}
