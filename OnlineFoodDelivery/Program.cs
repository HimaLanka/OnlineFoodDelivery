using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Services;
using OnlineFoodDelivery.Aspect;

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
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();  
builder.Services.AddScoped<IOrderService, OrderService>();         

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
