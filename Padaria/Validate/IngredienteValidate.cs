using PadariaAPI.DTO;
using System;

namespace PadariaAPI.Validate
{
    //classe IngredienteValidate implementa a validação dos dados do ingrediente
    public class IngredienteValidate : IIngredienteValidate
    {
        //método para validar um ingrediente
        public bool Validar(IngredienteDTO ingrediente)
        {
            //verifica se o ingrediente é nulo
            if (ingrediente == null)
                throw new ArgumentNullException("Ingrediente não pode ser nulo.");

            //verifica se o nome do ingrediente está vazio ou nulo
            if (string.IsNullOrWhiteSpace(ingrediente.Nome))
                throw new ArgumentException("Nome do ingrediente é obrigatório.");

            //valida o tamanho do nome do ingrediente 
            if (ingrediente.Nome.Length < 3 || ingrediente.Nome.Length > 100)
                throw new ArgumentException("Nome do ingrediente deve ter entre 3 e 100 caracteres.");

            //verifica se o preço do ingrediente é maior que zero
            if (ingrediente.Preco <= 0)
                throw new ArgumentException("O preço do ingrediente deve ser maior que zero.");

            //verifica se a validade do ingrediente não é anterior à data atual
            if (ingrediente.Validade < DateTime.Today)
                throw new ArgumentException("A validade do ingrediente não pode ser anterior à data atual.");

            //verifica se a unidade de medida está vazia ou nula
            if (string.IsNullOrWhiteSpace(ingrediente.UnidadeMedida))
                throw new ArgumentException("Unidade de medida é obrigatória.");

            //valida se a unidade de medida está entre as opções permitidas
            var unidadesValidas = new[] { "KG", "UNIDADE", "LITRO" };
            if (!Array.Exists(unidadesValidas, u => u.Equals(ingrediente.UnidadeMedida, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Unidade de medida inválida. As opções válidas são: KG, UNIDADE, LITRO.");

            return true; 
        }
    }
}
