using System;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Classes
{
    public class Venda
    {
        //armazena o identificador único da venda
        public int Id { get; set; }

        //lista de produtos que foram vendidos na venda
        public List<Produto> Produtos { get; set; } = new();

        //data da venda
        public DateTime DataVenda { get; set; } = DateTime.Today;

        //funcionário responsável por realizar a venda
        public Funcionario Funcionario { get; set; }

        //cliente que realizou a compra
        public Cliente Cliente { get; set; }

        // método para calcular o total da venda
        public decimal CalcularTotal()
        {
            //calcula a soma dos preços dos produtos, aplicando desconto de 50% se o produto vencer hoje
            return Produtos.Sum(p => p.Validade.Date == DataVenda.Date ? p.Preco * 0.5m : p.Preco);
        }
    }
}
