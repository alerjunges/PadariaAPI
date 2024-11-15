using PadariaAPI.DTO;

namespace PadariaAPI.Validate
{
    public interface IIngredienteValidate
    {
        //método para validar os dados de um ingrediente
        bool Validar(IngredienteDTO ingrediente);
    }
}
