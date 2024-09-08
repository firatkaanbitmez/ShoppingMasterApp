using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using ShoppingMasterApp.Infrastructure.Repositories;
using ShoppingMasterApp.API.Middlewares;
using ShoppingMasterApp.API.Filters;
using ShoppingMasterApp.Application.Mappings;
using ShoppingMasterApp.Application.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ShoppingMasterApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureMiddleware(app);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add controllers with filters (if required)
    services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();  // Global validation filter
        options.Filters.Add<LoggingFilter>();     // Global logging filter
        options.Filters.Add<ExceptionFilter>();   // Global exception filter

    });

    // Configure database context with retry policy and error handling
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Retry policy
        })
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)); // Improves performance for read-only queries

    // Add AutoMapper
    services.AddAutoMapper(typeof(AutoMapperProfile));

    // Add Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingMaster API", Version = "v1" });
        //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //c.IncludeXmlComments(xmlPath);
    });

    // Enable CORS globally
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });

    // Register services and repositories
    RegisterServices(services);
}

void RegisterServices(IServiceCollection services)
{
    // Application services
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IDiscountService, DiscountService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IPaymentService, PaymentService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IReviewService, ReviewService>();
    services.AddScoped<IShippingService, ShippingService>();
    services.AddScoped<IUserService, UserService>();

    // Repositories
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IDiscountRepository, DiscountRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IPaymentRepository, PaymentRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IReviewRepository, ReviewRepository>();
    services.AddScoped<IShippingRepository, ShippingRepository>();
    services.AddScoped<IUserRepository, UserRepository>();

    // Unit of Work pattern
    services.AddScoped<IUnitOfWork, UnitOfWork>();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingMaster API V1"));
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseAuthorization();

    // Add custom middleware
    app.UseMiddleware<ExceptionMiddleware>();
    app.MapControllers();
}
