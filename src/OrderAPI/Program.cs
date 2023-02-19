using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var npgsql = new NpgsqlConnectionStringBuilder();

npgsql.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
npgsql.Username = builder.Configuration["UserID"];
npgsql.Password = builder.Configuration["Password"];

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
builder.Services.AddDbContext<OrderContext>(opt =>
    opt.UseNpgsql(npgsql.ConnectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
