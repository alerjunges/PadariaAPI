using Microsoft.EntityFrameworkCore;
using PadariaAPI.Classes;
using PadariaAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe CompraRepository implementa os métodos da interface ICompraRepository
    public class CompraRepository : ICompraRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public CompraRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar uma compra pelo id
        public Compra ObterPorId(int id)
        {
            return _dbContext.Compras
                .Include(c => c.Ingredientes) 
                .Include(c => c.Fornecedor)  
                .Include(c => c.Funcionario) 
                .FirstOrDefault(c => c.Id == id);
        }

        //método para listar todas as compras
        public IEnumerable<Compra> ListarTodas()
        {
            return _dbContext.Compras
                .Include(c => c.Ingredientes) 
                .Include(c => c.Fornecedor)  
                .Include(c => c.Funcionario) 
                .ToList();
        }

        //método para adicionar uma compra
        public void Adicionar(Compra compra)
        {
            _dbContext.Compras.Add(compra);
            _dbContext.SaveChanges();
        }

        //método para atualizar uma compra
        public void Atualizar(Compra compra)
        {
            _dbContext.Compras.Update(compra);
            _dbContext.SaveChanges();
        }

        //método para remover uma compra
        public void Remover(Compra compra)
        {
            _dbContext.Compras.Remove(compra);
            _dbContext.SaveChanges();
        }
    }
}
