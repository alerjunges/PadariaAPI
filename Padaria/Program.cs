using Microsoft.EntityFrameworkCore;
using PadariaAPI.Data;
using Serilog;
using System.Text.Json.Serialization;

//configura o Serilog para registrar logs no console e em arquivo
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() //registra logs no console
    .WriteTo.File(
        path: "logs/log-.txt", //arquivo de logs rotacionado diariamente
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .MinimumLevel.Debug() //define o nível mínimo de log como Debug
    .CreateLogger();

try
{
    Log.Information("Inicializando aplicação..."); //log de início da aplicação

    var builder = WebApplication.CreateBuilder(args);

    //configura o Serilog como o provedor de logs
    builder.Host.UseSerilog();

    //adiciona o contexto do banco de dados em memória
    builder.Services.AddDbContext<InMemoryDbContext>(options =>
        options.UseInMemoryDatabase("PadariaDb"));

    //adiciona os controladores e configurações de JSON
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        Log.Information("Habilitando Swagger para o ambiente de desenvolvimento."); //log de habilitação do Swagger
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    Log.Information("Mapeando controladores."); //log de mapeamento dos controladores
    app.MapControllers();

    Log.Information("Aplicação iniciada com sucesso."); //log de sucesso na inicialização da aplicação
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação falhou ao iniciar."); //log de erro fatal na inicialização
}
finally
{
    Log.CloseAndFlush(); //garante que todos os logs são salvos antes de encerrar
}
