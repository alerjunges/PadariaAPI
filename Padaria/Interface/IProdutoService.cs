using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface IProdutoService
    {
        //método para obter um produto pelo id
        ProdutoDTO ObterPorId(int id);

        //método para listar todos os produtos
        IEnumerable<ProdutoDTO> ListarTodos();

        //método para adicionar um produto
        ProdutoDTO Adicionar(ProdutoDTO produtoDto);

        //método para atualizar um produto 
        void Atualizar(int id, ProdutoDTO produtoDto);

        //método para remover um produto
        void Remover(int id);
    }
}
