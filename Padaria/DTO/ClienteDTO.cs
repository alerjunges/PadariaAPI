using System;

namespace PadariaAPI.DTO
{
    //classe ClienteDTO representa um objeto de transferência de dados para Cliente
    public class ClienteDTO
    {
        //identificador único do cliente
        public int Id { get; set; }

        //nome do cliente
        public string Nome { get; set; }

        //email do cliente
        public string Email { get; set; }

        //telefone do cliente 
        public string Telefone { get; set; }

        //data de cadastro do cliente 
        public DateTime DataCadastro { get; set; }
    }
}
