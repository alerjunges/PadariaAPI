using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

//CompraController responsável por tratar os erros CompraService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly CompraService _compraService;

        //construtor da classe CompraController
        public ComprasController(InMemoryDbContext context)
        {
            //inicializa o CompraService com o contexto do banco de dados
            _compraService = new CompraService(context);
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            try
            {
                //obter uma compra pelo id
                var compra = _compraService.ObterCompraPorId(id);
                if (compra == null) //verifica se a compra não foi encontrada
                    return NotFound("Compra não encontrada."); //retorna erro 404

                return Ok(compra); //retorna status 200
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
                //lista de todas as compras
                var compras = _compraService.ListarTodas();
                return Ok(compras); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST
        public IActionResult RegistrarCompra([FromBody] CompraDTO compraDto)
        {
            try
            {
                //registrar uma nova compra
                _compraService.RegistrarCompra(compraDto);
                //retorna status 200
                return Ok("Compra registrada com sucesso.");
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
        public IActionResult AtualizarCompra(int id, [FromBody] CompraDTO compraDto)
        {
            try
            {
                //atualizar a compra 
                compraDto.Id = id;
                _compraService.AtualizarCompra(compraDto);
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
        public IActionResult RemoverCompra(int id)
        {
            try
            {
                //remover a compra 
                _compraService.RemoverCompra(id);
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
