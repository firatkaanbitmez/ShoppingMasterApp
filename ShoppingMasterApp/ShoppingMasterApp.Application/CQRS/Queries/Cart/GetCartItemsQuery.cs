using ShoppingMasterApp.Application.DTOs;

namespace ShoppingMasterApp.Application.CQRS.Queries.Cart
{
    public class GetCartItemsQuery 
    {
        public int UserId { get; set; }
    }
}
