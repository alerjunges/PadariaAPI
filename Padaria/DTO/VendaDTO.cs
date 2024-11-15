namespace PadariaAPI.DTO
{
    public class VendaDTO
    {
        //identificador único da venda
        public int Id { get; set; }

        //lista de identificadores dos produtos 
        public List<int> ProdutoIds { get; set; }

        //lista dos produtos incluídos na venda
        public List<ProdutoDTO> ProdutosDetalhados { get; set; }

        //data em que a venda foi realizada
        public DateTime DataVenda { get; set; }

        //identificador único do funcionário responsável pela venda
        public int? FuncionarioId { get; set; }

        //nome do funcionário responsável pela venda
        public string FuncionarioNome { get; set; }

        //identificador único do cliente associado a venda
        public int? ClienteId { get; set; }

        //nome do cliente associado a venda
        public string ClienteNome { get; set; }

        //valor total da venda
        public decimal Total { get; set; }
    }
}
