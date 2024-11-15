using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        //construtor da classe FuncionarioController
        public FuncionarioController(InMemoryDbContext context)
        {
            //inicializa o FuncionarioService com o contexto do banco de dados
            _funcionarioService = new FuncionarioService(context);
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodos()
        {
            try
            {
                //lista de todos os funcionários
                var funcionarios = _funcionarioService.ListarTodos();
                return Ok(funcionarios); //retorna status HTTP 200
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
                //obter um funcionário pelo id
                var funcionario = _funcionarioService.ObterPorId(id);
                if (funcionario == null) //verifica se o funcionário não foi encontrado
                    return NotFound("Funcionário não encontrado."); //retorna erro 404

                return Ok(funcionario); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] FuncionarioDTO funcionarioDto)
        {
            try
            {
                //adicionar um novo funcionário
                var funcionario = _funcionarioService.Adicionar(funcionarioDto);
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
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
        public IActionResult Atualizar(int id, [FromBody] FuncionarioDTO funcionarioDto)
        {
            try
            {
                //atualizar o funcionário 
                _funcionarioService.Atualizar(id, funcionarioDto);
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
                //remover o funcionário 
                _funcionarioService.Remover(id);
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
