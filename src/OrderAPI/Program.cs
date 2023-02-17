var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/h", () => "yoooo");

app.MapControllers();

app.Run();
