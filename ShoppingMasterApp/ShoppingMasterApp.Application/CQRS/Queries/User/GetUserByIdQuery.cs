using ShoppingMasterApp.Application.DTOs;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetUserByIdQuery
    {
        public int Id { get; set; }
    }

    public class GetUserByIdQueryResponse
    {
        public UserDto User { get; set; }
    }
}
