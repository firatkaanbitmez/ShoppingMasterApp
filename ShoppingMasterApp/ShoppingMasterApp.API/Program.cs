using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Application.Services;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using ShoppingMasterApp.Infrastructure.Repositories;
using ShoppingMasterApp.API.Middlewares;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Context setup
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper registration
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Swagger services
builder.Services.AddEndpointsApiExplorer(); // API için endpoint'leri keþfetmek
builder.Services.AddSwaggerGen(); // Swagger arayüzünü ekliyoruz

// Registering Services and Repositories for Dependency Injection
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Registering Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Build the app
var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Swagger middleware'i ekleyelim
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingMaster API V1"); // Swagger UI endpoint'i
    });
}

app.UseHttpsRedirection();

// Kendi Exception middleware'inizi burada kullanýyorsunuz
//app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Uygulamayý çalýþtýr
app.Run();
