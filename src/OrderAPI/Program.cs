using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.Data;
using Npgsql;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var npgsql = new NpgsqlConnectionStringBuilder();

npgsql.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
npgsql.Username = builder.Configuration["UserID"];
npgsql.Password = builder.Configuration["Password"];

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddDbContext<OrderContext>(opt =>
    opt.UseNpgsql(npgsql.ConnectionString));
builder.Services.AddScoped<IOrderAPIRepo, SqlOrderAPIRepo>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5173/", "https://orders-frontend-khaki.vercel.app/")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order API",
        Description = "An ASP.NET Core Web API for managing Order items",
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
      opt.Audience = builder.Configuration["ResourceId"];
      opt.Authority = $"{builder.Configuration["AAD:Instance"]}{builder.Configuration["TenantId"]}";
    });

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseSwagger();
app.UseSwaggerUI();


app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => "hello, this is working");

app.MapControllers();

app.Run();
