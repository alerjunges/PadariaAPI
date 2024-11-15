namespace PadariaAPI.DTO
{
    public class ProdutoDTO
    {
        //identificador único do produto
        public int Id { get; set; }

        //nome do produto
        public string Nome { get; set; }

        //preço do produto
        public decimal Preco { get; set; }

        //validade do produto
        public DateTime Validade { get; set; }

        //unidade de medida do produto
        public string UnidadeMedida { get; set; }
    }
}
