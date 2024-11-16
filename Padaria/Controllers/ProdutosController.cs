using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadariaAPI.DTO;
using PadariaAPI.Data;
using PadariaAPI.Services;
using System;

//ProdutoController responsável por tratar os erros ProdutoService
namespace PadariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        private readonly ILogger<ProdutosController> _logger;

        //construtor da classe ProdutoController
        public ProdutosController(InMemoryDbContext context, ILogger<ProdutosController> logger)
        {
            //inicializa o ProdutoService com o contexto do banco de dados
            _produtoService = new ProdutoService(context);
            _logger = logger; //inicializa o logger para registrar eventos no controlador
        }

        [HttpGet] //requisições GET 
        public IActionResult ListarTodos()
        {
            _logger.LogInformation("Iniciando a listagem de todos os produtos."); //log de início da listagem
            try
            {
                //lista de todos os produtos
                var produtos = _produtoService.ListarTodos();
                _logger.LogInformation("Listagem de produtos concluída com sucesso."); //log informando sucesso na listagem
                return Ok(produtos); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar os produtos."); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")] //requisições GET com parâmetro id 
        public IActionResult ObterPorId(int id)
        {
            _logger.LogInformation("Iniciando a busca do produto com ID: {Id}.", id); //log de início da busca por ID
            try
            {
                //obter um produto pelo id
                var produto = _produtoService.ObterPorId(id);
                if (produto == null) //verifica se o produto não foi encontrado
                {
                    _logger.LogWarning("Produto com ID: {Id} não encontrado.", id); //log de aviso se o produto não for encontrado
                    return NotFound("Produto não encontrado."); //retorna erro 404
                }

                _logger.LogInformation("Produto com ID: {Id} encontrado com sucesso.", id); //log informando sucesso ao encontrar o produto
                return Ok(produto); //retorna status HTTP 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto com ID: {Id}.", id); //log de erro caso ocorra falha
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost] //requisições POST 
        public IActionResult Adicionar([FromBody] ProdutoDTO produtoDto)
        {
            _logger.LogInformation("Iniciando o processo de adicionar um novo produto."); //log de início do processo de adição
            try
            {
                //adicionar um novo produto
                _logger.LogInformation("Tentando adicionar produto com os dados: {@ProdutoDTO}.", produtoDto); //log dos dados fornecidos
                var produto = _produtoService.Adicionar(produtoDto);
                _logger.LogInformation("Produto adicionado com sucesso com ID: {Id}.", produto.Id); //log informando sucesso na adição
                //retorna status 201
                return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao adicionar produto: {@ProdutoDTO}.", produtoDto); //log de aviso em caso de erro de validação
                //retorna erro 400 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao adicionar produto."); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //requisições PUT com parâmetro id
        public IActionResult Atualizar(int id, [FromBody] ProdutoDTO produtoDto)
        {
            _logger.LogInformation("Iniciando a atualização do produto com ID: {Id}.", id); //log de início da atualização
            try
            {
                //atualizar o produto
                _logger.LogInformation("Tentando atualizar produto com os dados: {@ProdutoDTO}.", produtoDto); //log dos dados fornecidos
                _produtoService.Atualizar(id, produtoDto);
                _logger.LogInformation("Produto com ID: {Id} atualizado com sucesso.", id); //log informando sucesso na atualização
                //retorna status 204
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao atualizar produto com ID: {Id}.", id); //log de aviso em caso de erro de validação
                //retorna erro 400
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar produto com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] //requisições DELETE com parâmetro id
        public IActionResult Remover(int id)
        {
            _logger.LogInformation("Iniciando a remoção do produto com ID: {Id}.", id); //log de início da remoção
            try
            {
                //remover o produto 
                _produtoService.Remover(id);
                _logger.LogInformation("Produto com ID: {Id} removido com sucesso.", id); //log informando sucesso na remoção
                //retorna status 204
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover produto com ID: {Id}.", id); //log de erro caso ocorra falha inesperada
                //retorna erro 500 
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
