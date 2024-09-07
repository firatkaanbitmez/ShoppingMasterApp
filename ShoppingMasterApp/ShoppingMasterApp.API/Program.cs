using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Application.Services;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using ShoppingMasterApp.Infrastructure.Repositories;
using ShoppingMasterApp.API.Middlewares;
using ShoppingMasterApp.API.Filters;
using ShoppingMasterApp.Application.Mappings;
using MediatR;
using ShoppingMasterApp.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();  // Global validation filter
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper, MediatR, Swagger, etc.
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repositories
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingMaster API V1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>(); // Custom Exception Handling
app.UseAuthorization();

app.MapControllers();
app.Run();
