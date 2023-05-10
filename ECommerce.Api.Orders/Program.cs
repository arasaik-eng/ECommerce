using ECommerce.Api.Orders.Data;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<OrderDbContext>(options =>
    {
        options.UseInMemoryDatabase("Orders");
    });
    builder.Services.AddScoped<IOrdersProvider, OrdersProvider>();
    builder.Services.AddAutoMapper(typeof(Program));
}





var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}