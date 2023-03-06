using System;
using CoreSB.Domain.Currency;
using CoreSB.Startup;
using CoreSB.Universal.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
StartupRegistrations.ConfigureAutoMapper();
StartupRegistrations.OptionsBinding(builder.Configuration);


// Universal services registration
RegistrationsIoC.ConfigureMainServices(builder.Services);

// Domain registration
Registration.ConfigureCurrencyServices(builder.Services, builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.CustomBeforeStartProcess();

app.Run();
