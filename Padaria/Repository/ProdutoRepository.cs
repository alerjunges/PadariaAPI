using PadariaAPI.Data;
using PadariaAPI.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe ProdutoRepository implementa os métodos da interface IProdutoRepository
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public ProdutoRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar um produto pelo id
        public Produto ObterPorId(int id)
        {
            return _dbContext.Produtos.Find(id); 
        }

        //método para listar todos os produtos
        public IEnumerable<Produto> ListarTodos()
        {
            return _dbContext.Produtos.ToList(); 
        }

        //método para adicionar um produto
        public void Adicionar(Produto produto)
        {
            _dbContext.Produtos.Add(produto); 
            _dbContext.SaveChanges(); 
        }

        //método para atualizar um produto
        public void Atualizar(Produto produto)
        {
            _dbContext.Produtos.Update(produto); 
            _dbContext.SaveChanges(); 
        }

        //método para remover um produto
        public void Remover(Produto produto)
        {
            _dbContext.Produtos.Remove(produto); 
            _dbContext.SaveChanges(); 
        }
    }
}
