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
using ShoppingMasterApp.Infrastructure.Seeders;
using ShoppingMasterApp.Application.Services;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

var builder = WebApplication.CreateBuilder(args);

// Serilog 
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Graylog(new GraylogSinkOptions
    {
        HostnameOrAddress = "127.0.0.1", // Localhost kullanýyorsanýz
        Port = 12201,                    // Graylog'un UDP portu
        TransportType = TransportType.Udp
    })
    .CreateLogger();



// Serilog'u kullanacak þekilde host'u yapýlandýrýn
builder.Host.UseSerilog();

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var adminSeeder = scope.ServiceProvider.GetRequiredService<AdminSeeder>();
    await adminSeeder.SeedAdminAsync();
}

ConfigureMiddleware(app);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers(options =>
    {
        //options.Filters.Add<ValidationFilter>();  // Global validation filter
        //options.Filters.Add<LoggingFilter>();     // Global logging filter
        //options.Filters.Add<ExceptionFilter>();   // Global exception filter
    });

    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        })
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


    services.AddAutoMapper(typeof(AutoMapperProfile));

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

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
    services.AddScoped<AdminSeeder>();
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
    services.AddScoped<IAdminRepository, AdminRepository>();

    // Verification Services
    services.AddScoped<ISmsVerificationService, SmsVerificationService>();
    services.AddScoped<IEmailVerificationService, EmailVerificationService>();
    services.AddScoped<IVerificationService, VerificationService>();


    // Unit of Work 
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Register the JWT token generator
    services.AddScoped<ITokenService, TokenService>();
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
    app.UseCors("AllowAllOrigins");
    app.UseAuthentication();
    app.UseAuthorization();

    // Add custom middleware 
    //app.UseMiddleware<RequestResponseLoggingMiddleware>();
    //app.UseMiddleware<ExceptionMiddleware>();

  
    app.UseSerilogRequestLogging();
    app.MapControllers();
}
