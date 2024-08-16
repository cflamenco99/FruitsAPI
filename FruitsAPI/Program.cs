using BusinessLogic.Fruits;
using DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

string DBUUID = Guid.NewGuid().ToString();
builder.Services.AddDbContext<FruitContext>(opt =>
    opt.UseInMemoryDatabase($"FruitList{DBUUID}")
);

builder.Services.AddScoped<IFruitRepository, FruitRepository>();

builder.Services.AddScoped<IBLFruit, BLFruit>();

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