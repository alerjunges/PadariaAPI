using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

//FuncionarioController responsável por tratar os erros FuncionarioService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;
        private readonly ILogger<FuncionariosController> _logger;

        //construtor da classe FuncionarioController
        public FuncionariosController(InMemoryDbContext context, ILogger<FuncionariosController> logger)
        {
            //inicializa o FuncionarioService com o contexto do banco de dados
            _funcionarioService = new FuncionarioService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodos()
        {
            _logger.LogInformation("Iniciando a listagem de todos os funcionários."); //log de início da listagem
            try
            {
                //lista de todos os funcionários
                var funcionarios = _funcionarioService.ListarTodos();
                _logger.LogInformation("Listagem de funcionários concluída com sucesso."); //log informando sucesso na listagem
                return Ok(funcionarios); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os funcionários."); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id 
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando a busca do funcionário com ID: {Id}.", id); //log de início da busca por ID
            try
            {
                //obter um funcionário pelo id
                var funcionario = _funcionarioService.ObterPorId(id);
                if (funcionario == null) //verifica se o funcionário não foi encontrado
                {
                    _logger.LogWarning("Funcionário com ID: {Id} não encontrado.", id); //log de aviso se o funcionário não for encontrado
                    return NotFound("Funcionário não encontrado."); //retorna erro 404
                }

                _logger.LogInformation("Funcionário com ID: {Id} encontrado com sucesso.", id); //log informando sucesso ao encontrar o funcionário
                return Ok(funcionario); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar funcionário com ID: {Id}.", id); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] FuncionarioDTO funcionarioDto)
        {
            _logger.LogInformation("Iniciando o processo de adicionar um novo funcionário."); //log de início do processo de adição
            try
            {
                //adicionar um novo funcionário
                _logger.LogInformation("Tentando adicionar funcionário com os dados: {@FuncionarioDTO}", funcionarioDto); //log dos dados fornecidos
                var funcionario = _funcionarioService.Adicionar(funcionarioDto);
                _logger.LogInformation("Funcionário adicionado com sucesso com ID: {Id}.", funcionario.Id); //log informando sucesso na adição
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao adicionar funcionário: {@FuncionarioDTO}", funcionarioDto); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao adicionar funcionário."); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult Atualizar(int id, [FromBody] FuncionarioDTO funcionarioDto)
        {
            _logger.LogInformation("Iniciando a atualização do funcionário com ID: {Id}.", id); //log de início da atualização
            try
            {
                //atualizar o funcionário 
                _logger.LogInformation("Tentando atualizar funcionário com os dados: {@FuncionarioDTO}", funcionarioDto); //log dos dados fornecidos
                _funcionarioService.Atualizar(id, funcionarioDto);
                _logger.LogInformation("Funcionário com ID: {Id} atualizado com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar funcionário com ID: {Id}.", id); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar funcionário com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult Remover(int id)
        {
            _logger.LogInformation("Iniciando a remoção do funcionário com ID: {Id}.", id); //log de início da remoção
            try
            {
                //remover o funcionário 
                _funcionarioService.Remover(id);
                _logger.LogInformation("Funcionário com ID: {Id} removido com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover funcionário com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
