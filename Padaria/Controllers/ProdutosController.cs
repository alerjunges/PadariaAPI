using Microsoft.AspNetCore.Mvc;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        //construtor da classe ProdutoController
        public ProdutoController(InMemoryDbContext context)
        {
            //inicializa o ProdutoService com o contexto do banco de dados
            _produtoService = new ProdutoService(context);
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodos()
        {
            try
            {
                //lista de todos os produtos
                var produtos = _produtoService.ListarTodos();
                return Ok(produtos); //retorna status HTTP 200
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
                //obter um produto pelo id
                var produto = _produtoService.ObterPorId(id);
                if (produto == null) //verifica se o produto não foi encontrado
                    return NotFound("Produto não encontrado."); //retorna erro 404

                return Ok(produto); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                //adicionar um novo produto
                var produto = _produtoService.Adicionar(produtoDto);
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
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
        public IActionResult Atualizar(int id, [FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                //atualizar o produto
                _produtoService.Atualizar(id, produtoDto);
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
                //remover o produto 
                _produtoService.Remover(id);
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
