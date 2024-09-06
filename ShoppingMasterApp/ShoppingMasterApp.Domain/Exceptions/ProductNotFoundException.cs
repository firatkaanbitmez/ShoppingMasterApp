namespace ShoppingMasterApp.Domain.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int id) : base($"Product with id {id} not found.") { }
    }
}
