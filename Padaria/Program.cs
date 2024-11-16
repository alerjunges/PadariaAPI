using Microsoft.EntityFrameworkCore;
using PadariaAPI.Data;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//ele serve como um intermedi�rio entre a aplica��o e o banco de dados
builder.Services.AddDbContext<InMemoryDbContext>(options =>
    //configurando para usar um banco de dados em mem�ria
    options.UseInMemoryDatabase("PadariaDb"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
