using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

//FornecedorController responsável por tratar os erros FornecedorService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly FornecedorService _fornecedorService;
        private readonly ILogger<FornecedoresController> _logger;

        //construtor da classe FornecedorController
        public FornecedoresController(InMemoryDbContext context, ILogger<FornecedoresController> logger)
        {
            //inicializa o FornecedorService com o contexto do banco de dados
            _fornecedorService = new FornecedorService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodos()
        {
            _logger.LogInformation("Iniciando a listagem de todos os fornecedores."); //log de início da listagem
            try
            {
                //lista de todos os fornecedores
                var fornecedores = _fornecedorService.ListarTodos();
                _logger.LogInformation("Listagem de fornecedores concluída com sucesso."); //log informando sucesso na listagem
                return Ok(fornecedores); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os fornecedores."); //log de erro caso ocorra falha
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando a busca do fornecedor com ID: {Id}", id); //log de início da busca por ID
            try
            {
                //obter um fornecedor pelo id
                var fornecedor = _fornecedorService.ObterPorId(id);
                if (fornecedor == null) //verifica se o fornecedor não foi encontrado
                {
                    _logger.LogWarning("Fornecedor com ID: {Id} não encontrado.", id); //log de aviso se o fornecedor não for encontrado
                    return NotFound("Fornecedor não encontrado."); //retorna erro 404
                }

                _logger.LogInformation("Fornecedor com ID: {Id} encontrado com sucesso.", id); //log informando sucesso ao encontrar o fornecedor
                return Ok(fornecedor); //retorna status 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar fornecedor com ID: {Id}.", id); //log de erro caso ocorra falha
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] FornecedorDTO fornecedorDto)
        {
            _logger.LogInformation("Iniciando o processo de adicionar um novo fornecedor."); //log de início do processo de adição
            try
            {
                //adicionar um novo fornecedor
                _logger.LogInformation("Tentando adicionar fornecedor com os dados: {@FornecedorDTO}", fornecedorDto); //log dos dados fornecidos
                var fornecedor = _fornecedorService.Adicionar(fornecedorDto);
                _logger.LogInformation("Fornecedor adicionado com sucesso com ID: {Id}.", fornecedor.Id); //log informando sucesso na adição
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = fornecedor.Id }, fornecedor);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao adicionar fornecedor: {@FornecedorDTO}", fornecedorDto); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao adicionar fornecedor."); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult Atualizar(int id, [FromBody] FornecedorDTO fornecedorDto)
        {
            _logger.LogInformation("Iniciando a atualização do fornecedor com ID: {Id}.", id); //log de início da atualização
            try
            {
                //atualizar o fornecedor
                _logger.LogInformation("Tentando atualizar fornecedor com os dados: {@FornecedorDTO}", fornecedorDto); //log dos dados fornecidos
                _fornecedorService.Atualizar(id, fornecedorDto);
                _logger.LogInformation("Fornecedor com ID: {Id} atualizado com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar fornecedor com ID: {Id}.", id); //log de aviso em caso de erro de validação
                // retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar fornecedor com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                // retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult Remover(int id)
        {
            _logger.LogInformation("Iniciando a remoção do fornecedor com ID: {Id}.", id); //log de início da remoção
            try
            {
                //remover o fornecedor 
                _fornecedorService.Remover(id);
                _logger.LogInformation("Fornecedor com ID: {Id} removido com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover fornecedor com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
