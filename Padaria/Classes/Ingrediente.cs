using System;

namespace PadariaAPI.Classes
{
    public class Ingrediente
    {
        //armazena o identificador único do ingrediente
        public int Id { get; set; }

        //armazena o nome do ingrediente
        public string Nome { get; set; }

        //preço do ingrediente
        public decimal Preco { get; set; }

        //data de validade do ingrediente
        public DateTime Validade { get; set; }

        //unidade de medida do ingrediente
        public string UnidadeMedida { get; set; }

        //calcula o valor do ingrediente
        public decimal CalcularValor()
        {
            return Preco;
        }
    }
}
