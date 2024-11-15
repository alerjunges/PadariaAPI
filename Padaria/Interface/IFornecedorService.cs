using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface IFornecedorService
    {
        //método para obter um fornecedor pelo id
        FornecedorDTO ObterPorId(int id);

        //método para listar todos os fornecedores
        IEnumerable<FornecedorDTO> ListarTodos();

        //método para adicionar um fornecedor
        FornecedorDTO Adicionar(FornecedorDTO fornecedorDto);

        //método para atualizar um fornecedor 
        void Atualizar(int id, FornecedorDTO fornecedorDto);

        //método para remover um fornecedor 
        void Remover(int id);
    }
}
