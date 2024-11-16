using System;

namespace PadariaAPI.Classes
{
    public class Cliente
    {
        //armazena o identificador único de cada cliente
        public int Id { get; set; }

        //armazena o nome do cliente 
        public string Nome { get; set; }

        //armazena o e-mail do cliente
        public string Email { get; set; }

        //armazena o número de telefone do cliente
        public string Telefone { get; set; }

        //armazena a data de quando o cliente foi cadastrado
        //DateTime.Today para pegar a data atual automaticamente
        public DateTime DataCadastro { get; set; } = DateTime.Today;

        //esse método calcula o tempo que o cliente está cadastrado em anos
        public int CalcularTempoComoCliente()
        {
            return (int)((DateTime.Today - DataCadastro).TotalDays / 365);
        }

        //esse método verifica se o cliente é recente
        public bool ClienteRecente()
        {
            //DateTime.Today para pegar a data atual e calcula a diferença em dias
            return (DateTime.Today - DataCadastro).TotalDays <= 30;
        }
    }
}
