using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using ShoppingMasterApp.Infrastructure.Repositories;
using ShoppingMasterApp.API.Middlewares;
using ShoppingMasterApp.API.Filters;
using ShoppingMasterApp.Application.Mappings;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using ShoppingMasterApp.Application.CQRS.Commands.Customer;
using ShoppingMasterApp.Application.CQRS.Queries.Customer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureMiddleware(app);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add controllers and global filters
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
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        })
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

    // Add AutoMapper for DTO to entity mappings
    services.AddAutoMapper(typeof(AutoMapperProfile));
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

    // Add Swagger for API documentation
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingMaster API", Version = "v1" });

        // JWT token'ý Swagger UI üzerinden göndermek için
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    });


    // Enable CORS globally
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });

    // Add JWT Authentication
    var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

    // Register services and repositories
    RegisterServices(services);
}

void RegisterServices(IServiceCollection services)
{
    // Repositories
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IDiscountRepository, DiscountRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IPaymentRepository, PaymentRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IReviewRepository, ReviewRepository>();
    services.AddScoped<IShippingRepository, ShippingRepository>();
    services.AddScoped<ICustomerRepository, CustomerRepository>();

    // Unit of Work pattern
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Register the JWT token generator
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
}

void ConfigureMiddleware(WebApplication app)
{
    // Use Swagger in development mode
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingMaster API V1"));
    }

    app.UseHttpsRedirection();

    // Enable CORS
    app.UseCors("AllowAllOrigins");

    // Enable JWT authentication
    app.UseAuthentication();

    app.UseAuthorization();

    // Add custom middleware for exception handling
    app.UseMiddleware<ExceptionMiddleware>();

    app.MapControllers();
}
