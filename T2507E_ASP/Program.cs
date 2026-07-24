using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.Services;
using T2507E_ASP.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddControllers();
// builder.Services.AddScoped<IPaymentService, MomoPaymentService>();
builder.Services.AddKeyedScoped<IPaymentService, MomoPaymentService>("momo");
builder.Services.AddKeyedScoped<IPaymentService, VnPayPaymentService>("vnpay");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Please make sure the connection string is set");
builder.Services.AddDbContext<T2507EASPDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
