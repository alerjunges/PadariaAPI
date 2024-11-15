using PadariaAPI.Classes;

namespace PadariaAPI.Validate
{
    public interface IVendaValidate
    {
        //método para validar os dados de uma venda
        bool Validar(Venda venda);
    }
}
