using System;

namespace PadariaAPI.Classes
{
    public class Fornecedor
    {
        //armazena o identificador único do fornecedor
        public int Id { get; set; }

        //armazena o nome do fornecedor
        public string Nome { get; set; }

        //armazenar o CNPJ do fornecedor
        public string CNPJ { get; set; }

        // armazena o telefone do fornecedor
        public string Telefone { get; set; }

        //armazena o e-mail do fornecedor
        public string Email { get; set; }

        //armazena a data de cadastro do fornecedor
        //DateTime.Today para que comece com a data atual
        public DateTime DataCadastro { get; set; } = DateTime.Today;

        //calcula quanto tempo o fornecedor está cadastrado
        public int CalcularTempoComoFornecedor()
        {
            return (int)((DateTime.Today - DataCadastro).TotalDays / 365);
        }
    }
}

