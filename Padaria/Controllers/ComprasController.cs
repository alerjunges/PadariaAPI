using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;
using Microsoft.Extensions.Logging;

//CompraController responsável por tratar os erros CompraService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly CompraService _compraService;
        private readonly ILogger<ComprasController> _logger;

        //construtor da classe CompraController
        public ComprasController(InMemoryDbContext context, ILogger<ComprasController> logger)
        {
            //inicializa o CompraService com o contexto do banco de dados
            _compraService = new CompraService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando a busca de uma compra com ID: {Id}", id); //log de início da busca por ID
            try
            {
                //obter uma compra pelo id
                var compra = _compraService.ObterCompraPorId(id);
                if (compra == null) //verifica se a compra não foi encontrada
                {
                    _logger.LogWarning("Compra com ID: {Id} não encontrada.", id); //log de aviso se a compra não for encontrada
                    return NotFound("Compra não encontrada."); //retorna erro 404
                }

                _logger.LogInformation("Compra com ID: {Id} encontrada com sucesso.", id); //log informando sucesso ao encontrar a compra
                return Ok(compra); //retorna status 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar compra com ID: {Id}.", id); //log de erro caso ocorra falha
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet] //requisições GET
        public IActionResult ListarTodas()
        {
            _logger.LogInformation("Iniciando a listagem de todas as compras."); //log de início da listagem
            try
            {
                //lista de todas as compras
                var compras = _compraService.ListarTodas();
                _logger.LogInformation("Listagem de todas as compras concluída com sucesso."); //log informando sucesso na listagem
                return Ok(compras); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar todas as compras."); //log de erro caso ocorra falha
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST
        public IActionResult RegistrarCompra([FromBody] CompraDTO compraDto)
        {
            _logger.LogInformation("Iniciando o registro de uma nova compra."); //log de início do processo de registro
            try
            {
                //registrar uma nova compra
                _logger.LogInformation("Tentando registrar a compra com os dados: {@CompraDTO}", compraDto); //log dos dados fornecidos
                _compraService.RegistrarCompra(compraDto);
                _logger.LogInformation("Compra registrada com sucesso."); //log informando sucesso no registro
                //retorna status 200
                return Ok("Compra registrada com sucesso.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao registrar a compra: {@CompraDTO}", compraDto); //log de aviso em caso de erro de validação
                //retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao registrar a compra."); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult AtualizarCompra(int id, [FromBody] CompraDTO compraDto)
        {
            _logger.LogInformation("Iniciando a atualização da compra com ID: {Id}.", id); //log de início da atualização
            try
            {
                //atualizar a compra 
                _logger.LogInformation("Tentando atualizar a compra com os dados: {@CompraDTO}", compraDto); //log dos dados fornecidos
                compraDto.Id = id;
                _compraService.AtualizarCompra(compraDto);
                _logger.LogInformation("Compra com ID: {Id} atualizada com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar a compra com ID: {Id}.", id); //log de aviso em caso de erro de validação
                //retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar a compra com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult RemoverCompra(int id)
        {
            _logger.LogInformation("Iniciando a remoção da compra com ID: {Id}.", id); //log de início da remoção
            try
            {
                //remover a compra 
                _compraService.RemoverCompra(id);
                _logger.LogInformation("Compra com ID: {Id} removida com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover a compra com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
