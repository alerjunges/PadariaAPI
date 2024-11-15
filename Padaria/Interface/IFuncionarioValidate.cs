using PadariaAPI.DTO;

namespace PadariaAPI.Validate
{
    public interface IFuncionarioValidate
    {
        //método para validar os dados de um funcionário
        bool Validar(FuncionarioDTO funcionario);
    }
}
