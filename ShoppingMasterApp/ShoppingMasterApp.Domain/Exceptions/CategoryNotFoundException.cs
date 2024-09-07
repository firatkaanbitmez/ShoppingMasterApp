using System;

namespace ShoppingMasterApp.Domain.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int id)
            : base($"Category with ID {id} not found.")
        {
        }
    }
}
