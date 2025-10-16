using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using OnlineFoodDelivery.Aspect;
using OnlineFoodDelivery.Auth;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Database connection
builder.Services.AddDbContext<OnlineFoodDeliveryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineFoodDeliveryContext")
        ?? throw new InvalidOperationException("Connection string 'OnlineFoodDeliveryContext' not found.")));

// Global exception handler
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionHandlerAttribute>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service registrations
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();  
builder.Services.AddScoped<IOrderService, OrderService>();

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.(middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();//do these 
app.UseAuthentication();
app.UseAuthorization();
//we can create our own middlewares to handle exceptions
//cross origin resource sharing to map other urls
app.MapControllers();//to handle end points
app.Run();//termination

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
