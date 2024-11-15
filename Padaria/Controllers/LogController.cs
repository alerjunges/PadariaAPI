using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;

        //construtor da classe LogController
        public LogController(InMemoryDbContext context)
        {
            //inicializa o LogService com o contexto do banco de dados
            _logService = new LogService(context);
        }

        [HttpGet] //requisições GET
        public IActionResult ListarTodos()
        {
            try
            {
                //lista de todos os logs
                var logs = _logService.ListarTodos();
                return Ok(logs); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST
        public IActionResult Adicionar([FromBody] LogDTO logDto)
        {
            try
            {
                //adicionar um novo log
                var log = _logService.Adicionar(logDto);
                //retorna status 200 
                return Ok(log);
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
