using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.Entities;
using T2507E_ASP.Repositories;
using T2507E_ASP.Repositories.Impl;
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

// Add scoped Repository
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add scoped Service
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Add Scoped AUTH
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// add Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
