using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Data;
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
builder.Services.AddScoped<IOrderAPIRepo, SqlOrderAPIRepo>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
