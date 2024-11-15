using PadariaAPI.Data;
using PadariaAPI.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe FuncionarioRepository implementa os métodos da interface IFuncionarioRepository
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public FuncionarioRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar um funcionário pelo id
        public Funcionario ObterPorId(int id)
        {
            return _dbContext.Funcionarios.FirstOrDefault(f => f.Id == id);
        }

        //método para listar todos os funcionários
        public IEnumerable<Funcionario> ListarTodos()
        {
            return _dbContext.Funcionarios.ToList();
        }

        //método para adicionar um funcionário
        public void Adicionar(Funcionario funcionario)
        {
            _dbContext.Funcionarios.Add(funcionario);
            _dbContext.SaveChanges();
        }

        //método para atualizar um funcionário
        public void Atualizar(Funcionario funcionario)
        {
            _dbContext.Funcionarios.Update(funcionario);
            _dbContext.SaveChanges();
        }

        //método para remover um funcionário
        public void Remover(Funcionario funcionario)
        {
            _dbContext.Funcionarios.Remove(funcionario);
            _dbContext.SaveChanges();
        }
    }
}
