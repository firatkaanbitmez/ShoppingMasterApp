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
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<ChangeProductStockCommand, Product>().ReverseMap();

            // User Mappings
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserProfileCommand, User>().ReverseMap();

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
