using AnimalAPI.Data;
using Microsoft.EntityFrameworkCore;
using AnimalAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
var mysqlVersion = new MySqlServerVersion(new Version(8, 0, 30));

builder.Services.AddDbContext<AnimalDbContext>(db => db.UseMySql(connectionString, mysqlVersion));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapAnimalsEndpoints();
app.MapAddressessEndpoints();
app.MapOwnersEndpoints();


app.Run();
