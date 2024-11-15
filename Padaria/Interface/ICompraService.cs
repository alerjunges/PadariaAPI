using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface ICompraService
    {
        //método para obter uma compra pelo id
        CompraDTO ObterCompraPorId(int id);

        //método para listar todas as compras
        IEnumerable<CompraDTO> ListarTodas();

        //método para registrar uma compra
        void RegistrarCompra(CompraDTO compraDto);

        //método para atualizar uma compra 
        void AtualizarCompra(CompraDTO compraDto);

        //método para remover uma compra pelo id
        void RemoverCompra(int id);
    }
}
