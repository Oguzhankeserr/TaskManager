using TaskManager.Business.Application;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure;
using TaskManager.Business.Infrastructure.UnitOfWork;
using TaskManager.Business.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddApplicationBusinessServices();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseHttpsRedirection();


app.UseAuthorization();

InfrastructureBusinessServiceRegistration.Migration(app.Services.CreateScope());

app.MapControllers();

app.Run();
