using PadariaAPI.DTO;
using System;
using System.Text.RegularExpressions;

namespace PadariaAPI.Validate
{
    //classe ClienteValidate implementa a validação dos dados do cliente
    public class ClienteValidate : IClienteValidate
    {
        //método para validar um cliente
        public bool Validar(ClienteDTO cliente)
        {
            //verifica se o cliente é nulo
            if (cliente == null)
                throw new ArgumentNullException("Cliente não pode ser nulo.");

            //verifica se o nome do cliente está vazio ou nulo
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new ArgumentException("O nome do cliente é obrigatório.");

            //valida o tamanho do nome para garantir um mínimo de caracteres
            if (cliente.Nome.Length < 3)
                throw new ArgumentException("O nome do cliente deve ter pelo menos 3 caracteres.");

            //verifica se o email está vazio ou nulo
            if (string.IsNullOrWhiteSpace(cliente.Email))
                throw new ArgumentException("O email do cliente é obrigatório.");

            //método ValidarEmail para verificar se o email está no formato correto
            if (!ValidarEmail(cliente.Email))
                throw new ArgumentException("O email do cliente não é válido.");

            //verifica se o telefone está vazio ou nulo
            if (string.IsNullOrWhiteSpace(cliente.Telefone))
                throw new ArgumentException("O telefone do cliente é obrigatório.");

            //método ValidarTelefone para verificar o formato do telefone
            if (!ValidarTelefone(cliente.Telefone))
                throw new ArgumentException("O telefone do cliente não é válido.");

            //verifica se a data de cadastro foi preenchida corretamente
            if (cliente.DataCadastro == default)
                throw new ArgumentException("A data de cadastro é obrigatória e deve ser válida.");

            //verifica se a data de cadastro não está no futuro
            if (cliente.DataCadastro > DateTime.Today)
                throw new ArgumentException("A data de cadastro não pode ser uma data futura.");

            return true;
        }

        //método para validar o formato de email 
        private bool ValidarEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email); 
        }

        // método para validar o formato do telefone 
        private bool ValidarTelefone(string telefone)
        {
            var telefoneRegex = new Regex(@"^\+?[0-9\s-]{8,15}$");
            return telefoneRegex.IsMatch(telefone); 
        }
    }
}
