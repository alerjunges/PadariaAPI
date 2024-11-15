using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface IFornecedorRepository
    {
        //método para obter um fornecedor pelo id
        Fornecedor ObterPorId(int id);

        //método para listar todos os fornecedores
        IEnumerable<Fornecedor> ListarTodos();

        //método para adicionar um fornecedor
        void Adicionar(Fornecedor fornecedor);

        //método para atualizar um fornecedor existente
        void Atualizar(Fornecedor fornecedor);

        //método para remover um fornecedor
        void Remover(Fornecedor fornecedor);
    }
}
