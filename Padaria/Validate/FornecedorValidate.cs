using PadariaAPI.DTO;
using System;
using System.Text.RegularExpressions;

namespace PadariaAPI.Validate
{
    //classe FornecedorValidate implementa a validação dos dados do fornecedor
    public class FornecedorValidate : IFornecedorValidate
    {
        //método para validar um fornecedor
        public bool Validar(FornecedorDTO fornecedor)
        {
            //verifica se o fornecedor é nulo
            if (fornecedor == null)
                throw new ArgumentNullException("Fornecedor não pode ser nulo.");

            //verifica se o nome do fornecedor está vazio ou nulo
            if (string.IsNullOrWhiteSpace(fornecedor.Nome))
                throw new ArgumentException("O nome do fornecedor é obrigatório.");

            //valida o tamanho do nome para garantir um mínimo de caracteres
            if (fornecedor.Nome.Length < 3)
                throw new ArgumentException("O nome do fornecedor deve ter pelo menos 3 caracteres.");

            //verifica se o CNPJ está vazio ou nulo
            if (string.IsNullOrWhiteSpace(fornecedor.CNPJ))
                throw new ArgumentException("O CNPJ do fornecedor é obrigatório.");

            //verifica se o CNPJ está no formato correto
            if (!ValidarCNPJ(fornecedor.CNPJ))
                throw new ArgumentException("O CNPJ do fornecedor não é válido.");

            //verifica se o email está vazio ou nulo
            if (string.IsNullOrWhiteSpace(fornecedor.Email))
                throw new ArgumentException("O email do fornecedor é obrigatório.");

            //verifica se o email está no formato correto
            if (!ValidarEmail(fornecedor.Email))
                throw new ArgumentException("O email do fornecedor não é válido.");

            //verifica se o telefone está vazio ou nulo
            if (string.IsNullOrWhiteSpace(fornecedor.Telefone))
                throw new ArgumentException("O telefone do fornecedor é obrigatório.");

            //verifica se o telefone está no formato correto
            if (!ValidarTelefone(fornecedor.Telefone))
                throw new ArgumentException("O telefone do fornecedor não é válido.");

            //verifica se a data de cadastro foi preenchida corretamente
            if (fornecedor.DataCadastro == default)
                throw new ArgumentException("A data de cadastro é obrigatória e deve ser válida.");

            //verifica se a data de cadastro não está no futuro
            if (fornecedor.DataCadastro > DateTime.Today)
                throw new ArgumentException("A data de cadastro não pode ser uma data futura.");

            return true; 
        }

        //método para validar o formato de email
        private bool ValidarEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        //método para validar o formato do telefone
        private bool ValidarTelefone(string telefone)
        {
            var telefoneRegex = new Regex(@"^\+?[0-9\s-]{8,15}$");
            return telefoneRegex.IsMatch(telefone);
        }

        //método para validar o CNPJ
        private bool ValidarCNPJ(string cnpj)
        {
            //remove caracteres não numéricos do CNPJ
            cnpj = Regex.Replace(cnpj, @"[^0-9]", "");

            //verifica se o tamanho do CNPJ é válido
            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma, resto;

            //valida o primeiro dígito verificador
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            if (resto != int.Parse(cnpj[12].ToString()))
                return false;

            //valida o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            return resto == int.Parse(cnpj[13].ToString());
        }
    }
}
