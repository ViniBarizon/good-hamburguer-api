using GoodHamburger.Application.UseCases.Orders;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Persistence;
using GoodHamburger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=goodhamburger.db"));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<ListOrdersUseCase>();
builder.Services.AddScoped<GetOrderUseCase>();
builder.Services.AddScoped<UpdateOrderUseCase>();
builder.Services.AddScoped<DeleteOrderUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();