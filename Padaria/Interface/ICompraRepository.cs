using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface ICompraRepository
    {
        //método para obter uma compra pelo id
        Compra ObterPorId(int id);

        //método para listar todas as compras
        IEnumerable<Compra> ListarTodas();

        //método para adicionar uma nova compra
        void Adicionar(Compra compra);

        //método para atualizar uma compra 
        void Atualizar(Compra compra);

        //método para remover uma compra
        void Remover(Compra compra);
    }
}
