using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

//IngredienteController responsável por tratar os erros IngredienteService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesController : ControllerBase
    {
        private readonly IngredienteService _ingredienteService;
        private readonly ILogger<IngredientesController> _logger;

        //construtor da classe IngredienteController
        public IngredientesController(InMemoryDbContext context, ILogger<IngredientesController> logger)
        {
            //inicializa o IngredienteService com o contexto do banco de dados
            _ingredienteService = new IngredienteService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet] //requisições GET
        public IActionResult ListarTodos()
        {
            _logger.LogInformation("Iniciando a listagem de todos os ingredientes."); //log de início da listagem
            try
            {
                //lista de todos os ingredientes
                var ingredientes = _ingredienteService.ListarTodos();
                _logger.LogInformation("Listagem de ingredientes concluída com sucesso."); //log informando sucesso na listagem
                return Ok(ingredientes); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os ingredientes."); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando a busca do ingrediente com ID: {Id}.", id); //log de início da busca por ID
            try
            {
                //obter um ingrediente 
                var ingrediente = _ingredienteService.ObterPorId(id);
                if (ingrediente == null) //verifica se o ingrediente não foi encontrado
                {
                    _logger.LogWarning("Ingrediente com ID: {Id} não encontrado.", id); //log de aviso se o ingrediente não for encontrado
                    return NotFound("Ingrediente não encontrado."); //retorna erro 404
                }

                _logger.LogInformation("Ingrediente com ID: {Id} encontrado com sucesso.", id); //log informando sucesso ao encontrar o ingrediente
                return Ok(ingrediente); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar ingrediente com ID: {Id}.", id); //log de erro caso ocorra falha
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] IngredienteDTO ingredienteDto)
        {
            _logger.LogInformation("Iniciando o processo de adicionar um novo ingrediente."); //log de início do processo de adição
            try
            {
                //adicionar um novo ingrediente
                _logger.LogInformation("Tentando adicionar ingrediente com os dados: {@IngredienteDTO}.", ingredienteDto); //log dos dados fornecidos
                var ingrediente = _ingredienteService.Adicionar(ingredienteDto);
                _logger.LogInformation("Ingrediente adicionado com sucesso com ID: {Id}.", ingrediente.Id); //log informando sucesso na adição
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = ingrediente.Id }, ingrediente);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao adicionar ingrediente: {@IngredienteDTO}.", ingredienteDto); //log de aviso em caso de erro de validação
                //retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao adicionar ingrediente."); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult Atualizar(int id, [FromBody] IngredienteDTO ingredienteDto)
        {
            _logger.LogInformation("Iniciando a atualização do ingrediente com ID: {Id}.", id); //log de início da atualização
            try
            {
                //atualizar o ingrediente 
                _logger.LogInformation("Tentando atualizar ingrediente com os dados: {@IngredienteDTO}.", ingredienteDto); //log dos dados fornecidos
                _ingredienteService.Atualizar(id, ingredienteDto);
                _logger.LogInformation("Ingrediente com ID: {Id} atualizado com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar ingrediente com ID: {Id}.", id); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar ingrediente com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult Remover(int id)
        {
            _logger.LogInformation("Iniciando a remoção do ingrediente com ID: {Id}.", id); //log de início da remoção
            try
            {
                //remover o ingrediente 
                _ingredienteService.Remover(id);
                _logger.LogInformation("Ingrediente com ID: {Id} removido com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover ingrediente com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
