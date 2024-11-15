using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface IIngredienteRepository
    {
        //método para obter um ingrediente pelo id
        Ingrediente ObterPorId(int id);

        //método para listar todos os ingredientes
        IEnumerable<Ingrediente> ListarTodos();

        //método para adicionar um ingrediente
        void Adicionar(Ingrediente ingrediente);

        //método para atualizar um ingrediente
        void Atualizar(Ingrediente ingrediente);

        //método para remover um ingrediente
        void Remover(Ingrediente ingrediente);
    }
}
