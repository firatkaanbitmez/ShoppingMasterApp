using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> FindByConditionAsync(Expression<System.Func<User, bool>> predicate);
    }
}
