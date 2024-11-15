using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteController : ControllerBase
    {
        private readonly IngredienteService _ingredienteService;

        //construtor da classe IngredienteController
        public IngredienteController(InMemoryDbContext context)
        {
            //inicializa o IngredienteService com o contexto do banco de dados
            _ingredienteService = new IngredienteService(context);
        }

        [HttpGet] //requisições GET
        public IActionResult ListarTodos()
        {
            try
            {
                //lista de todos os ingredientes
                var ingredientes = _ingredienteService.ListarTodos();
                return Ok(ingredientes); //retorna status HTTP 200
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
                //obter um ingrediente 
                var ingrediente = _ingredienteService.ObterPorId(id);
                if (ingrediente == null) //verifica se o ingrediente não foi encontrado
                    return NotFound("Ingrediente não encontrado."); //retorna erro 404

                return Ok(ingrediente); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] IngredienteDTO ingredienteDto)
        {
            try
            {
                //adicionar um novo ingrediente
                var ingrediente = _ingredienteService.Adicionar(ingredienteDto);
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = ingrediente.Id }, ingrediente);
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
        public IActionResult Atualizar(int id, [FromBody] IngredienteDTO ingredienteDto)
        {
            try
            {
                //atualizar o ingrediente 
                _ingredienteService.Atualizar(id, ingredienteDto);
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
                //remover o ingrediente 
                _ingredienteService.Remover(id);
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
