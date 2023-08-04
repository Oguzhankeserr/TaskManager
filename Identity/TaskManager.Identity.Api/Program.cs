using Microsoft.EntityFrameworkCore;
using TaskManager.Identity.Infrastructure.Context;
using TaskManager.Identity.Infrastructure.Persistence;
using TaskManager.Identity.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RabbitMQ.Client;
using Microsoft.AspNet.Identity;
using TaskManager.Identity.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
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
            ValidateAudience = true, //who will use the token  www.site.com
            ValidateIssuer = true, //who is giving the token   www.myapi.com
            ValidateLifetime = true, //checking the token life time
            ValidateIssuerSigningKey = true, //validate that this is my key

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<EmailConsumerService>();

//builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddApplicationServices();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(options =>
           options.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
InfrastructureServiceRegistration.Migration(app.Services.CreateScope());

app.MapControllers();

app.Run();
