using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<OrderContext>(opt =>
    opt.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
