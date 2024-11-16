using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly ILogger<ClientesController> _logger;

        //construtor da classe ClienteController
        public ClientesController(InMemoryDbContext context, ILogger<ClientesController> logger)
        {
            //inicializa o ClienteService com o contexto do banco de dados
            _clienteService = new ClienteService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet] //requisições GET
        public IActionResult ListarTodos()
        {
            _logger.LogInformation("Iniciando processo de listagem de todos os clientes."); //log de início da listagem

            try
            {
                //lista de todos os clientes 
                var clientes = _clienteService.ListarTodos();
                _logger.LogInformation("Listagem de clientes concluída com sucesso. Total de clientes: {Count}", clientes?.Count()); //log informando sucesso e quantidade de clientes
                return Ok(clientes); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao listar clientes. Exceção: {ExceptionMessage}", ex.Message); //log de erro caso ocorra falha
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando processo para obter cliente pelo ID: {ClientId}.", id); //log de início da busca por ID

            try
            {
                //obter um cliente pelo id
                var cliente = _clienteService.ObterPorId(id);
                if (cliente == null) //verifica se o cliente não foi encontrado
                {
                    _logger.LogWarning("Cliente não encontrado para o ID: {ClientId}.", id); //log de aviso se o cliente não for encontrado
                    return NotFound("Cliente não encontrado."); //retorna erro 404
                }

                _logger.LogInformation("Cliente com ID: {ClientId} encontrado com sucesso.", id); //log informando sucesso ao encontrar o cliente
                return Ok(cliente); //retorna status 200
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter cliente pelo ID: {ClientId}. Exceção: {ExceptionMessage}", id, ex.Message); //log de erro caso ocorra falha
                //retorna um erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST
        public IActionResult Adicionar([FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Iniciando processo de adição de cliente."); //log de início do processo de adição

            try
            {
                //adicionar um novo cliente
                var cliente = _clienteService.Adicionar(clienteDto);
                _logger.LogInformation("Cliente adicionado com sucesso. ID: {ClientId}, Nome: {ClientName}", cliente.Id, cliente.Nome); //log informando sucesso na adição
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Erro de validação ao adicionar cliente. Exceção: {ExceptionMessage}", ex.Message); //log de aviso se ocorrer erro de validação
                //retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro inesperado ao adicionar cliente. Exceção: {ExceptionMessage}", ex.Message); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult Atualizar(int id, [FromBody] ClienteDTO clienteDto)
        {
            _logger.LogInformation("Iniciando processo de atualização do cliente com ID: {ClientId}.", id); //log de início da atualização

            try
            {
                //atualizar o cliente 
                _clienteService.Atualizar(id, clienteDto);
                _logger.LogInformation("Cliente com ID: {ClientId} atualizado com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Erro de validação ao atualizar cliente com ID: {ClientId}. Exceção: {ExceptionMessage}", id, ex.Message); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro inesperado ao atualizar cliente com ID: {ClientId}. Exceção: {ExceptionMessage}", id, ex.Message); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult Remover(int id)
        {
            _logger.LogInformation("Iniciando processo de remoção do cliente com ID: {ClientId}.", id); //log de início da remoção

            try
            {
                //remover o cliente 
                _clienteService.Remover(id);
                _logger.LogInformation("Cliente com ID: {ClientId} removido com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro inesperado ao remover cliente com ID: {ClientId}. Exceção: {ExceptionMessage}", id, ex.Message); //log de erro caso ocorra falha inesperada
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
