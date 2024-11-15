using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface IProdutoRepository
    {
        //método para obter um produto pelo id
        Produto ObterPorId(int id);

        //método para listar todos os produtos
        IEnumerable<Produto> ListarTodos();

        //método para adicionar um produto
        void Adicionar(Produto produto);

        //método para atualizar um produto
        void Atualizar(Produto produto);

        //método para remover um produto
        void Remover(Produto produto);
    }
}
