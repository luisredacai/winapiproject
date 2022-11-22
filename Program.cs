using ApiProject2.Context;
using ApiProject2.Models;
using Microsoft.EntityFrameworkCore;
//using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

  var connectionString2 = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<CrmContext>(options => options.UseSqlServer(connectionString2));

  var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSQLServer");
    builder.Services.AddDbContext<da8k2ujd2nc9e6Context>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
