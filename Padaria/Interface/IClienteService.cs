using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface IClienteService
    {
        //método para obter um cliente pelo id
        ClienteDTO ObterPorId(int id);

        //método para listar todos os clientes
        IEnumerable<ClienteDTO> ListarTodos();

        //método para adicionar um cliente 
        ClienteDTO Adicionar(ClienteDTO clienteDto);

        //método para atualizar um cliente 
        void Atualizar(int id, ClienteDTO clienteDto);

        //método para remover um cliente pelo id
        void Remover(int id);
    }
}
