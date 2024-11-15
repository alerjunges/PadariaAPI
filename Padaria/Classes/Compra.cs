using System;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Classes
{
    public class Compra
    {
        //armazena o identificador único de cada compra
        public int Id { get; set; }

        //List<Ingrediente> para armazenar vários itens

        public List<Ingrediente> Ingredientes { get; set; } = new();

        //armazena a data em que a compra foi feita
        // DateTime.Today para pegar a data atual automaticamente
        public DateTime DataCompra { get; set; } = DateTime.Today;

        //armazena o fornecedor que fez a venda dos ingredientes
        public Fornecedor Fornecedor { get; set; }

        //armazena informações do funcionário que realizou a compra
        public Funcionario Funcionario { get; set; }

        // calcula o total da compra, somando o preço de cada ingrediente da lista `Ingredientes`
        public decimal CalcularTotal()
        {
            return Ingredientes.Sum(i => i.Preco);
        }
    }
}
