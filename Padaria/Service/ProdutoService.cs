using PadariaAPI.Classes;
using PadariaAPI.Data;
using PadariaAPI.DTO;
using PadariaAPI.Repository;
using PadariaAPI.Validate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Services
{
    //classe ProdutoService é responsável por controlar as regras de negócios
    public class ProdutoService : IProdutoService
    {
        private readonly ProdutoRepository _produtoRepository; //repositório para acessar os dados de produtos
        private readonly ProdutoValidate _produtoValidate; //validador para verificar os dados do produto

        //construtor recebe o contexto do banco e inicializa o repositório e o validador
        public ProdutoService(InMemoryDbContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
            _produtoValidate = new ProdutoValidate();
        }

        //método para buscar um produto pelo id
        public ProdutoDTO ObterPorId(int id)
        {
            var produto = _produtoRepository.ObterPorId(id); //busca o produto no repositório
            if (produto == null)
                throw new ArgumentException("Produto não encontrado."); //exceção se o produto não existir

            //retorna um DTO com os dados do produto
            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = CalcularPrecoComDesconto(produto), //calcula o preço com desconto
                Validade = produto.Validade,
                UnidadeMedida = produto.UnidadeMedida
            };
        }

        //método para listar todos os produtos
        public IEnumerable<ProdutoDTO> ListarTodos()
        {
            //retorna um DTO com os dados do produto
            return _produtoRepository.ListarTodos().Select(produto => new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = CalcularPrecoComDesconto(produto), //calcula o preço com desconto para cada produto
                Validade = produto.Validade,
                UnidadeMedida = produto.UnidadeMedida
            });
        }

        //método para adicionar um produto
        public ProdutoDTO Adicionar(ProdutoDTO produtoDto)
        {
            //valida os dados do produto
            if (!_produtoValidate.Validar(produtoDto))
                throw new ArgumentException("Dados inválidos para o produto."); //exceção se a validação falhar

            //cria uma entidade Produto a partir do DTO
            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                Validade = produtoDto.Validade,
                UnidadeMedida = produtoDto.UnidadeMedida
            };

            _produtoRepository.Adicionar(produto);
            produtoDto.Id = produto.Id;
            return produtoDto; 
        }

        //método para atualizar um produto
        public void Atualizar(int id, ProdutoDTO produtoDto)
        {
            var produtoExistente = _produtoRepository.ObterPorId(id); //busca o produto no repositório
            if (produtoExistente == null)
                throw new ArgumentException("Produto não encontrado."); //exceção se o produto não existir

            //valida os dados do produto
            if (!_produtoValidate.Validar(produtoDto))
                throw new ArgumentException("Dados inválidos para o produto."); //exceção se a validação falhar

            //atualiza os dados do produto
            produtoExistente.Nome = produtoDto.Nome;
            produtoExistente.Preco = produtoDto.Preco;
            produtoExistente.Validade = produtoDto.Validade;
            produtoExistente.UnidadeMedida = produtoDto.UnidadeMedida;

            _produtoRepository.Atualizar(produtoExistente);
        }

        //método para remover um produto
        public void Remover(int id)
        {
            var produto = _produtoRepository.ObterPorId(id); //busca o produto no repositório
            if (produto == null)
                throw new ArgumentException("Produto não encontrado."); //exceção se o produto não existir

            _produtoRepository.Remover(produto);
        }

        //método para calcular o preço com desconto
        private decimal CalcularPrecoComDesconto(Produto produto)
        {
            //se a validade for hoje, aplica um desconto de 50%
            return produto.Validade.Date == DateTime.Today ? produto.Preco * 0.5m : produto.Preco;
        }
    }
}
