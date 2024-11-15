using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _vendaService;

        //construtor da classe VendaController
        public VendaController(InMemoryDbContext context)
        {
            //inicializa o VendaService com o contexto do banco de dados
            _vendaService = new VendaService(context);
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            try
            {
                //obter uma venda pelo id
                var venda = _vendaService.ObterVendaPorId(id);
                if (venda == null) //verifica se a venda não foi encontrada
                    return NotFound("Venda não encontrada."); //retorna erro 404

                return Ok(venda); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodas()
        {
            try
            {
                //lista de todas as vendas
                var vendas = _vendaService.ListarTodas();
                return Ok(vendas); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult RegistrarVenda([FromBody] VendaDTO vendaDto)
        {
            try
            {
                //registra uma nova venda
                _vendaService.RegistrarVenda(vendaDto);
                //retorna status 200 
                return Ok("Venda registrada com sucesso.");
            }
            catch (ArgumentException ex)
            {
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult AtualizarVenda(int id, [FromBody] VendaDTO vendaDto)
        {
            try
            {
                if (vendaDto == null)
                    return BadRequest("Os dados da venda não podem ser nulos."); 

                vendaDto.Id = id;

                _vendaService.AtualizarVenda(id, vendaDto);

                //retorna status 204 
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult RemoverVenda(int id)
        {
            try
            {
                //remove a venda 
                _vendaService.RemoverVenda(id);
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
