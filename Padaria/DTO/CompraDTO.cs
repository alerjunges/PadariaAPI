using System;
using System.Collections.Generic;

namespace PadariaAPI.DTO
{
    public class CompraDTO
    {
        //identificador único da compra
        public int Id { get; set; }

        //lista de ids dos ingredientes na compra
        public List<int> IngredienteIds { get; set; }

        //lista detalhada dos ingredientes
        public List<IngredienteDTO> IngredientesDetalhados { get; set; }

        //data em que a compra foi realizada
        public DateTime DataCompra { get; set; }

        //identificador do fornecedor relacionado à compra
        public int? FornecedorId { get; set; }

        //nome do fornecedor relacionado à compra
        public string FornecedorNome { get; set; }

        //identificador do funcionário que realizou a compra
        public int? FuncionarioId { get; set; }

        //nome do funcionário que realizou a compra
        public string FuncionarioNome { get; set; }

        //valor total da compra
        public decimal Total { get; set; }
    }
}
