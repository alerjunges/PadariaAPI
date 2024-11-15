using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface IVendaRepository
    {
        //método para obter uma venda pelo id
        Venda ObterPorId(int id);

        //método para listar todas as vendas
        IEnumerable<Venda> ListarTodas();

        //método para adicionar uma venda
        void Adicionar(Venda venda);

        //método para atualizar uma venda 
        void Atualizar(Venda venda);

        //método para remover uma venda
        void Remover(Venda venda);
    }
}
