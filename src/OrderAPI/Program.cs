using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Data;
using Npgsql;
using Microsoft.OpenApi.Models;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order API",
        Description = "An ASP.NET Core Web API for managing Order items",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapGet("/health", () => Results.Ok());

app.MapControllers();

app.Run();
