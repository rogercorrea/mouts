using DeveloperStore.Application.UseCases.CreateSale;
using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<CreateSaleUseCase>();

builder.Services.AddScoped<ISaleRepository, FakeSaleRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();