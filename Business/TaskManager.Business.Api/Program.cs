using TaskManager.Business.Application;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure;
using TaskManager.Business.Infrastructure.UnitOfWork;
using TaskManager.Business.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Business.Infrastructure.Services.Storage.Local;
using TaskManager.Business.Infrastructure.Services.Storage.Azure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, 
            ValidateIssuer = true, 
            ValidateLifetime = true, 
            ValidateIssuerSigningKey = true, 

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });
builder.Services.AddBusinessInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationBusinessServices();

builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

//app.UseCors(options =>
//           options.WithOrigins("http://localhost:4200")
//           .AllowAnyMethod()
//           .AllowAnyHeader());

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

InfrastructureBusinessServiceRegistration.Migration(app.Services.CreateScope());

app.MapControllers();

app.Run();
