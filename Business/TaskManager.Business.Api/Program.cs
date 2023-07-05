using TaskManager.Business.Application;
using TaskManager.Business.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBusinessInfrastructureServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationBusinessServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

InfrastructureBusinessServiceRegistration.Migration(app.Services.CreateScope());

app.MapControllers();

app.Run();
