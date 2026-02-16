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

// CORS de la pagina web.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Inyección de dependencias

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

// Commands
builder.Services.AddScoped<IDishCommand, DishCommand>();
builder.Services.AddScoped<IOrderCommand, OrderCommand>();
builder.Services.AddScoped<IOrderItemCommand, OrderItemCommand>();

// Queries
builder.Services.AddScoped<IDishQuery, DishQuery>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();
builder.Services.AddScoped<IOrderItemQuery, OrderItemQuery>();
builder.Services.AddScoped<IStatusQuery, StatusQuery>();
builder.Services.AddScoped<IDeliveryTypeQuery, DeliveryTypeQuery>();
builder.Services.AddScoped<IOrderQuery, OrderQuery>();

// Mappers
builder.Services.AddScoped<IDishMapper, DishMapper>();
builder.Services.AddScoped<IOrderMapper, OrderMapper>();

// Services
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
