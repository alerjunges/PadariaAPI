using System;

namespace PadariaAPI.DTO
{
    public class FornecedorDTO
    {
        //identificador único do fornecedor
        public int Id { get; set; }

        //nome do fornecedor
        public string Nome { get; set; }

        //cnpj do fornecedor
        public string CNPJ { get; set; }

        //telefone do fornecedor
        public string Telefone { get; set; }

        //email do fornecedor
        public string Email { get; set; }

        //data em que o fornecedor foi cadastrado 
        public DateTime DataCadastro { get; set; }
    }
}
