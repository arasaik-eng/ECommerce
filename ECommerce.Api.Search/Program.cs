using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ISearchService, SearchService>();
    builder.Services.AddScoped<IOrdersService, OrdersService>();
    builder.Services.AddScoped<IProductsService, ProductsService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();

    builder.Services.AddHttpClient("OrderService", config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
    });
    builder.Services.AddHttpClient("ProductsService", config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
    }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMicroseconds(500)));
    builder.Services.AddHttpClient("CustomerService", config =>
    {
        config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
    }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMicroseconds(500)));
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