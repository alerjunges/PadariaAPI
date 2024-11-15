using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface IClienteRepository
    {
        //método para obter um cliente pelo id
        Cliente ObterPorId(int id);

        //método para listar todos os clientes
        IEnumerable<Cliente> ListarTodos();

        //método para adicionar um novo cliente
        void Adicionar(Cliente cliente);

        //método para atualizar um cliente existente
        void Atualizar(Cliente cliente);

        //método para remover um cliente
        void Remover(Cliente cliente);
    }
}
