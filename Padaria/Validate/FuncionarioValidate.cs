using PadariaAPI.DTO;
using System;

namespace PadariaAPI.Validate
{
    //classe FuncionarioValidate implementa a validação dos dados do funcionário
    public class FuncionarioValidate : IFuncionarioValidate
    {
        //método para validar um funcionário
        public bool Validar(FuncionarioDTO funcionario)
        {
            //verifica se o funcionário é nulo
            if (funcionario == null)
                throw new ArgumentNullException("Funcionário não pode ser nulo.");

            //verifica se o nome do funcionário está vazio ou nulo
            if (string.IsNullOrWhiteSpace(funcionario.Nome))
                throw new ArgumentException("O nome do funcionário é obrigatório.");

            //valida o tamanho do nome para garantir um mínimo de caracteres
            if (funcionario.Nome.Length < 3)
                throw new ArgumentException("O nome do funcionário deve ter pelo menos 3 caracteres.");

            //verifica se o cargo está vazio ou nulo
            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                throw new ArgumentException("O cargo do funcionário é obrigatório.");

            //valida o tamanho do cargo para garantir um mínimo de caracteres
            if (funcionario.Cargo.Length < 3)
                throw new ArgumentException("O cargo do funcionário deve ter pelo menos 3 caracteres.");

            //verifica se o salário é positivo
            if (funcionario.Salario <= 0)
                throw new ArgumentException("O salário do funcionário deve ser um valor positivo.");

            //verifica se a data de admissão é válida
            if (funcionario.DataAdmissao == default)
                throw new ArgumentException("A data de admissão é obrigatória e deve ser válida.");

            //verifica se a data de admissão não está no futuro
            if (funcionario.DataAdmissao > DateTime.Today)
                throw new ArgumentException("A data de admissão não pode ser uma data futura.");

            //valida se a data de admissão não é muito antiga
            if (funcionario.DataAdmissao < DateTime.Today.AddYears(-50))
                throw new ArgumentException("A data de admissão é muito antiga. Verifique se está correta.");

            return true; 
        }
    }
}
