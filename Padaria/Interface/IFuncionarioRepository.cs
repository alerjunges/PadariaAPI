using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface IFuncionarioRepository
    {
        //método para obter um funcionário pelo id
        Funcionario ObterPorId(int id);

        //método para listar todos os funcionários
        IEnumerable<Funcionario> ListarTodos();

        //método para adicionar um funcionário
        void Adicionar(Funcionario funcionario);

        //método para atualizar um funcionário 
        void Atualizar(Funcionario funcionario);

        //método para remover um funcionário
        void Remover(Funcionario funcionario);
    }
}
