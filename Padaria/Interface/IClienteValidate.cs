using PadariaAPI.DTO;

namespace PadariaAPI.Validate
{
    public interface IClienteValidate
    {
        //método para validar os dados de um cliente
        bool Validar(ClienteDTO cliente);
    }
}
