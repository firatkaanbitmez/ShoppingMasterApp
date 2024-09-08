using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.CQRS.Commands.Payment;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.CQRS.Commands.Shipping;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Category Mappings
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap(); // ReverseMap for bidirectional mapping

            // Cart Mappings
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<AddToCartCommand, Cart>().ReverseMap();
            CreateMap<UpdateCartItemCommand, Cart>().ReverseMap();

            // Order Mappings
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<CreateOrderCommand, Order>().ReverseMap();
            CreateMap<UpdateOrderCommand, Order>().ReverseMap();
            CreateMap<AddOrderItemCommand, OrderItem>().ReverseMap();

            // Product Mappings
            CreateMap<ChangeProductStockCommand, Product>().ReverseMap();

            CreateMap<CreateProductCommand, Product>()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD"))); // Decimal to Money mapping
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD"))); // Decimal to Money mapping

            // User Mappings
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Email(src.Email))) // Map string to Email value object
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Address.AddressLine1, src.Address.AddressLine2, src.Address.City, src.Address.State, src.Address.PostalCode, src.Address.Country))); // Map Address object

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => new Email(src.Email))) // Map string to Email value object
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Address.AddressLine1, src.Address.AddressLine2, src.Address.City, src.Address.State, src.Address.PostalCode, src.Address.Country))); // Map Address object

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value)) // Map Email value object to string
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToString())); // Map Enum to string

            // Payment Mappings
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<ProcessPaymentCommand, Payment>().ReverseMap();

            // Review Mappings
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<AddReviewCommand, Review>().ReverseMap();
            CreateMap<UpdateReviewCommand, Review>().ReverseMap();

            // Shipping Mappings
            CreateMap<Shipping, ShippingDto>().ReverseMap();
            CreateMap<CreateShippingCommand, Shipping>().ReverseMap();
            CreateMap<UpdateShippingCommand, Shipping>().ReverseMap();
        }
    }
}
 