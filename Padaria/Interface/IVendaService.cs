using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface IVendaService
    {
        VendaDTO ObterVendaPorId(int id); // Definição do método para obter venda por ID
        IEnumerable<VendaDTO> ListarTodas(); // Definição do método para listar todas as vendas
        void RegistrarVenda(VendaDTO vendaDto); // Definição do método para registrar uma venda
        void AtualizarVenda(int id, VendaDTO vendaDto); // Definição do método para atualizar uma venda
        void RemoverVenda(int id); // Definição do método para remover uma venda
    }
}
