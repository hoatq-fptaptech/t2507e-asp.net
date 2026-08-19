using System.IO.IsolatedStorage;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using T2507E_ASP.Data;
using T2507E_ASP.Entities;
using T2507E_ASP.Mappings;
using T2507E_ASP.Messaging;
using T2507E_ASP.Repositories;
using T2507E_ASP.Repositories.Impl;
using T2507E_ASP.Services;
using T2507E_ASP.Services.Impl;
using T2507E_ASP.Storages;
using T2507E_ASP.Storages.Impl;

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization();
// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Provider
var storageProvider = builder.Configuration["FileStorage:Provider"];
switch (storageProvider)
{
    case "MinIO": builder.Services.AddScoped<IFileStorageProvider, MinioStorageProvider>();break;
    default: builder.Services.AddScoped<IFileStorageProvider, LocalStorageProvider>();break;
}
// Add Mapper
builder.Services.AddAutoMapper(typeof(StudentProfile));
// Add RabbitMQ Publisher
builder.Services.AddScoped<RabbitMqPublisher>();
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
app.UseAuthentication();
app.UseAuthorization();
app.Run();
