using PadariaAPI.Classes;
using PadariaAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe IngredienteRepository implementa os métodos da interface IIngredienteRepository
    public class IngredienteRepository : IIngredienteRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public IngredienteRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar um ingrediente pelo id
        public Ingrediente ObterPorId(int id)
        {
            return _dbContext.Ingredientes.Find(id);
        }

        //método para listar todos os ingredientes
        public IEnumerable<Ingrediente> ListarTodos()
        {
            return _dbContext.Ingredientes.ToList();
        }

        //método para adicionar um ingrediente
        public void Adicionar(Ingrediente ingrediente)
        {
            _dbContext.Ingredientes.Add(ingrediente);
            _dbContext.SaveChanges();
        }

        //método para atualizar um ingrediente
        public void Atualizar(Ingrediente ingrediente)
        {
            _dbContext.Ingredientes.Update(ingrediente);
            _dbContext.SaveChanges();
        }

        //método para remover um ingrediente
        public void Remover(Ingrediente ingrediente)
        {
            _dbContext.Ingredientes.Remove(ingrediente);
            _dbContext.SaveChanges();
        }
    }
}
