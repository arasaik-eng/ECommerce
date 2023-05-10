using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<CustomersDbContext>(options =>
    {
        options.UseInMemoryDatabase("Customers");
    });
    builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();
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