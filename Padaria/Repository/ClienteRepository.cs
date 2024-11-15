using PadariaAPI.Data;
using PadariaAPI.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe ClienteRepository implementa os métodos da interface IClienteRepository
    public class ClienteRepository : IClienteRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public ClienteRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar um cliente pelo id
        public Cliente ObterPorId(int id)
        {
            return _dbContext.Clientes.FirstOrDefault(c => c.Id == id);
        }

        //método para listar todos os clientes
        public IEnumerable<Cliente> ListarTodos()
        {
            return _dbContext.Clientes.ToList();
        }

        //método para adicionar um cliente 
        public void Adicionar(Cliente cliente)
        {
            _dbContext.Clientes.Add(cliente);
            _dbContext.SaveChanges();
        }

        //método para atualizar um cliente 
        public void Atualizar(Cliente cliente)
        {
            _dbContext.Clientes.Update(cliente);
            _dbContext.SaveChanges();
        }

        //método para remover um cliente 
        public void Remover(Cliente cliente)
        {
            _dbContext.Clientes.Remove(cliente);
            _dbContext.SaveChanges();
        }
    }
}
