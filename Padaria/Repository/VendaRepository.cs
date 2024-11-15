using Microsoft.EntityFrameworkCore;
using PadariaAPI.Classes;
using PadariaAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe VendaRepository implementa os métodos da interface IVendaRepository
    public class VendaRepository : IVendaRepository
    {
        private readonly InMemoryDbContext _dbContext; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência para trabalhar com os dados
        public VendaRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //método para buscar uma venda pelo id
        public Venda ObterPorId(int id)
        {
            //carrega as relações de produtos, funcionário e cliente
            return _dbContext.Vendas
                .Include(v => v.Produtos)
                .Include(v => v.Funcionario)
                .Include(v => v.Cliente)
                .FirstOrDefault(v => v.Id == id);
        }

        //método para listar todas as vendas
        public IEnumerable<Venda> ListarTodas()
        {
            //carrega as relações de produtos, funcionário e cliente
            return _dbContext.Vendas
                .Include(v => v.Produtos)
                .Include(v => v.Funcionario)
                .Include(v => v.Cliente)
                .ToList();
        }

        //método para adicionar uma venda
        public void Adicionar(Venda venda)
        {
            _dbContext.Vendas.Add(venda);
            _dbContext.SaveChanges();
        }

        //método para atualizar uma venda
        public void Atualizar(Venda venda)
        {
            _dbContext.Vendas.Update(venda);
            _dbContext.SaveChanges();
        }

        //método para remover uma venda
        public void Remover(Venda venda)
        {
            _dbContext.Vendas.Remove(venda);
            _dbContext.SaveChanges();
        }
    }
}
