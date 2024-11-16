using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

//VendaController responsável por tratar os erros VendaService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly VendaService _vendaService;
        private readonly ILogger<VendasController> _logger;

        //construtor da classe VendaController
        public VendasController(InMemoryDbContext context, ILogger<VendasController> logger)
        {
            //inicializa o VendaService com o contexto do banco de dados
            _vendaService = new VendaService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando a busca da venda com ID: {Id}.", id); //log de início da busca por ID
            try
            {
                //obter uma venda pelo id
                var venda = _vendaService.ObterVendaPorId(id);
                if (venda == null) //verifica se a venda não foi encontrada
                {
                    _logger.LogWarning("Venda com ID: {Id} não encontrada.", id); //log de aviso se a venda não for encontrada
                    return NotFound("Venda não encontrada."); //retorna erro 404
                }

                _logger.LogInformation("Venda com ID: {Id} encontrada com sucesso.", id); //log informando sucesso ao encontrar a venda
                return Ok(venda); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar venda com ID: {Id}.", id); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodas()
        {
            _logger.LogInformation("Iniciando a listagem de todas as vendas."); //log de início da listagem
            try
            {
                //lista de todas as vendas
                var vendas = _vendaService.ListarTodas();
                _logger.LogInformation("Listagem de vendas concluída com sucesso."); //log informando sucesso na listagem
                return Ok(vendas); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar vendas."); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult RegistrarVenda([FromBody] VendaDTO vendaDto)
        {
            _logger.LogInformation("Iniciando o registro de uma nova venda."); //log de início do processo de registro
            try
            {
                //registra uma nova venda
                _logger.LogInformation("Tentando registrar uma nova venda com os dados: {@VendaDTO}.", vendaDto); //log dos dados fornecidos
                _vendaService.RegistrarVenda(vendaDto);
                _logger.LogInformation("Venda registrada com sucesso."); //log informando sucesso no registro
                //retorna status 200 
                return Ok("Venda registrada com sucesso.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao registrar venda: {@VendaDTO}.", vendaDto); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao registrar venda."); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult AtualizarVenda(int id, [FromBody] VendaDTO vendaDto)
        {
            _logger.LogInformation("Iniciando a atualização da venda com ID: {Id}.", id); //log de início da atualização
            try
            {
                if (vendaDto == null) //verifica se os dados fornecidos são nulos
                {
                    _logger.LogWarning("Dados da venda estão nulos ao tentar atualizar venda com ID: {Id}.", id); //log de aviso
                    return BadRequest("Os dados da venda não podem ser nulos.");
                }

                vendaDto.Id = id;

                _logger.LogInformation("Tentando atualizar venda com os dados: {@VendaDTO}.", vendaDto); //log dos dados fornecidos
                _vendaService.AtualizarVenda(id, vendaDto);

                _logger.LogInformation("Venda com ID: {Id} atualizada com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204 
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar venda com ID: {Id}.", id); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar venda com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult RemoverVenda(int id)
        {
            _logger.LogInformation("Iniciando a remoção da venda com ID: {Id}.", id); //log de início da remoção
            try
            {
                //remove a venda 
                _vendaService.RemoverVenda(id);
                _logger.LogInformation("Venda com ID: {Id} removida com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover venda com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
