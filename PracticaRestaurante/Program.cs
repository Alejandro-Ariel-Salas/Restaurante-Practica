using Aplication.Interfaces.ICommand;
using Aplication.Interfaces.IMappers;
using Aplication.Interfaces.IQuery;
using Aplication.Interfaces.IService;
using Aplication.Mappers;
using Aplication.UseCases;
using Infraestructure.Command;
using Infraestructure.Perssistence;
using Infraestructure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

// Commands
builder.Services.AddScoped<IDishCommand, DishCommand>();

// Queries
builder.Services.AddScoped<IDishQuery, DishQuery>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();

// Mappers
builder.Services.AddScoped<IDishMapper, DishMapper>();

// Services
builder.Services.AddScoped<IDishService, DishService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
