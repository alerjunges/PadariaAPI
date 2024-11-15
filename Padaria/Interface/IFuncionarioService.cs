using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface IFuncionarioService
    {
        //método para obter um funcionário pelo id
        FuncionarioDTO ObterPorId(int id);

        //método para listar todos os funcionários
        IEnumerable<FuncionarioDTO> ListarTodos();

        //método para adicionar um funcionário
        FuncionarioDTO Adicionar(FuncionarioDTO funcionarioDto);

        //método para atualizar um funcionário 
        void Atualizar(int id, FuncionarioDTO funcionarioDto);

        //método para remover um funcionário
        void Remover(int id);
    }
}
