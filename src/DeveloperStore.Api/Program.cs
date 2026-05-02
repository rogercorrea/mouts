using DeveloperStore.Application.UseCases.CreateSale;
using DeveloperStore.Application.UseCases.CancelSale;
using DeveloperStore.Application.Interfaces;
using DeveloperStore.Infrastructure.Repositories;
using DeveloperStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<CreateSaleUseCase>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=developerstore.db"));

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<CreateSaleUseCase>();
builder.Services.AddScoped<CancelSaleUseCase>();

var app = builder.Build();

app.MapControllers();

app.Run();