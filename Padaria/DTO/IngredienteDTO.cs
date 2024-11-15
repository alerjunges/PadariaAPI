using System;

namespace PadariaAPI.DTO
{
    public class IngredienteDTO
    {
        //identificador único do ingrediente
        public int Id { get; set; }

        //nome do ingrediente
        public string Nome { get; set; }

        //preço do ingrediente
        public decimal Preco { get; set; }

        //validade do ingrediente
        public DateTime Validade { get; set; }

        //unidade de medida do ingrediente
        public string UnidadeMedida { get; set; }
    }
}
