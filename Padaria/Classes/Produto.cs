namespace PadariaAPI.Classes
{
    public class Produto
    {
        //armazena o identificador único do produto
        public int Id { get; set; }

        //armazena o nome do produto
        public string Nome { get; set; }

        //preço do produto

        public decimal Preco { get; set; }

        //data de validade do produto
        public DateTime Validade { get; set; }

        //unidade de medida do produto
        public string UnidadeMedida { get; set; }

        //calcula o valor do produto
        public decimal CalcularValor()
        {
            //verifica se a validade é hoje, se for, aplica um desconto de 50%
            //DateTime.Today para comparar apenas a data, sem considerar o horário
            return Validade.Date == DateTime.Today ? Preco * 0.5m : Preco;
        }
    }
}
