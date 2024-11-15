using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface IIngredienteService
    {
        //método para obter um ingrediente pelo id
        IngredienteDTO ObterPorId(int id);

        //método para listar todos os ingredientes
        IEnumerable<IngredienteDTO> ListarTodos();

        //método para adicionar um ingrediente
        IngredienteDTO Adicionar(IngredienteDTO ingredienteDto);

        //método para atualizar um ingrediente
        void Atualizar(int id, IngredienteDTO ingredienteDto);

        //método para remover um ingrediente
        void Remover(int id);
    }
}
