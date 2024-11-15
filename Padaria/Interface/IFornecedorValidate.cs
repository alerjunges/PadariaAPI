using PadariaAPI.DTO;

namespace PadariaAPI.Validate
{
    public interface IFornecedorValidate
    {
        //método para validar os dados de um fornecedor
        bool Validar(FornecedorDTO fornecedor);
    }
}
