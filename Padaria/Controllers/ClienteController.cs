using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        //construtor da classe ClienteController
        public ClienteController(InMemoryDbContext context) 
        {
            //inicializa o ClienteService com o contexto do banco de dados
            _clienteService = new ClienteService(context);
        }

        [HttpGet] //requisições GET
        public IActionResult ListarTodos()
        {
            try
            {
                //lista de todos os clientes 
                var clientes = _clienteService.ListarTodos();
                return Ok(clientes); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id
        public IActionResult ObterPorId(int id)
        {
            try
            {
                //obter um cliente pelo id
                var cliente = _clienteService.ObterPorId(id);
                if (cliente == null) //verifica se o cliente não foi encontrado
                    return NotFound("Cliente não encontrado."); //retorna erro 404

                return Ok(cliente); //retorna status 200
            }
            catch (Exception ex)
            {
                //retorna um erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST
        public IActionResult Adicionar([FromBody] ClienteDTO clienteDto)
        {
            try
            {
                //adicionar um novo cliente
                var cliente = _clienteService.Adicionar(clienteDto);
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
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
        public IActionResult Atualizar(int id, [FromBody] ClienteDTO clienteDto)
        {
            try
            {
                //atualizar o cliente 
                _clienteService.Atualizar(id, clienteDto);
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
        public IActionResult Remover(int id)
        {
            try
            {
                //remover o cliente 
                _clienteService.Remover(id);
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
