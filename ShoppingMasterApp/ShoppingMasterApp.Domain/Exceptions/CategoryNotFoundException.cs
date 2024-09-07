using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int id)
            : base($"Category with id {id} not found.") { }
    }
}
