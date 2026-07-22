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
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
