using PadariaAPI.Data;
using PadariaAPI.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe FornecedorRepository implementa os métodos da interface IFornecedorRepository
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public FornecedorRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar um fornecedor pelo id
        public Fornecedor ObterPorId(int id)
        {
            return _dbContext.Fornecedores.FirstOrDefault(f => f.Id == id);
        }

        //método para listar todos os fornecedores
        public IEnumerable<Fornecedor> ListarTodos()
        {
            return _dbContext.Fornecedores.ToList();
        }

        //método para adicionar um fornecedor
        public void Adicionar(Fornecedor fornecedor)
        {
            _dbContext.Fornecedores.Add(fornecedor);
            _dbContext.SaveChanges();
        }

        //método para atualizar um fornecedor
        public void Atualizar(Fornecedor fornecedor)
        {
            _dbContext.Fornecedores.Update(fornecedor);
            _dbContext.SaveChanges();
        }

        //método para remover um fornecedor
        public void Remover(Fornecedor fornecedor)
        {
            _dbContext.Fornecedores.Remove(fornecedor);
            _dbContext.SaveChanges();
        }
    }
}
