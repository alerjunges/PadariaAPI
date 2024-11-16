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
    .MinimumLevel.Debug() //define o n�vel m�nimo de log como Debug
    .CreateLogger();

try
{
    Log.Information("Inicializando aplica��o..."); //log de in�cio da aplica��o

    var builder = WebApplication.CreateBuilder(args);

    //configura o Serilog como o provedor de logs
    builder.Host.UseSerilog();

    //adiciona o contexto do banco de dados em mem�ria
    builder.Services.AddDbContext<InMemoryDbContext>(options =>
        options.UseInMemoryDatabase("PadariaDb"));

    //adiciona os controladores e configura��es de JSON
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        Log.Information("Habilitando Swagger para o ambiente de desenvolvimento."); //log de habilita��o do Swagger
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    Log.Information("Mapeando controladores."); //log de mapeamento dos controladores
    app.MapControllers();

    Log.Information("Aplica��o iniciada com sucesso."); //log de sucesso na inicializa��o da aplica��o
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplica��o falhou ao iniciar."); //log de erro fatal na inicializa��o
}
finally
{
    Log.CloseAndFlush(); //garante que todos os logs s�o salvos antes de encerrar
}
