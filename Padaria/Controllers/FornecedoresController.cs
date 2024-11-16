using Microsoft.AspNetCore.Mvc;
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

        //construtor da classe FornecedorController
        public FornecedoresController(InMemoryDbContext context)
        {
            //inicializa o FornecedorService com o contexto do banco de dados
            _fornecedorService = new FornecedorService(context);
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodos()
        {
            try
            {
                //lista de todos os fornecedores
                var fornecedores = _fornecedorService.ListarTodos();
                return Ok(fornecedores); //retorna status HTTP 200
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
                //obter um fornecedor pelo id
                var fornecedor = _fornecedorService.ObterPorId(id);
                if (fornecedor == null) //verifica se o fornecedor não foi encontrado
                    return NotFound("Fornecedor não encontrado."); //retorna erro 404

                return Ok(fornecedor); //retorna status 200
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] FornecedorDTO fornecedorDto)
        {
            try
            {
                //adicionar um novo fornecedor
                var fornecedor = _fornecedorService.Adicionar(fornecedorDto);
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = fornecedor.Id }, fornecedor);
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
        public IActionResult Atualizar(int id, [FromBody] FornecedorDTO fornecedorDto)
        {
            try
            {
                //atualizar o fornecedor
                _fornecedorService.Atualizar(id, fornecedorDto);
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult Remover(int id)
        {
            try
            {
                //remover o fornecedor 
                _fornecedorService.Remover(id);
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
