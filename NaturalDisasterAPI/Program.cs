using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

if (connectionString == null) throw new Exception("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

var app = builder.Build();

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
