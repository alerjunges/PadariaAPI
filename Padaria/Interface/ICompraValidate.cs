using PadariaAPI.Classes;

namespace PadariaAPI.Validate
{
    public interface ICompraValidate
    {
        //método para validar os dados de uma compra
        bool Validar(Compra compra);
    }
}
