using PadariaAPI.DTO;

namespace PadariaAPI.Validate
{
    public interface IProdutoValidate
    {
        //método para validar os dados de um produto
        bool Validar(ProdutoDTO produto);
    }
}
