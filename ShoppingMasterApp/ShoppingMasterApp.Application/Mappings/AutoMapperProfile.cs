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
            // Map entities to DTOs and commands
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))  
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));  


            // Mapping CreateProductCommand to Product, with proper Price mapping
            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => new ProductDetails(src.Manufacturer, src.Sku)))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD"))); 


            // Mapping UpdateProductCommand to Product, with proper Price mapping
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD")));

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Shipping, ShippingDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Email, string>().ConvertUsing(email => email.Value);

            // Map value objects
            CreateMap<Money, MoneyDto>().ReverseMap();
        }
    }
}
