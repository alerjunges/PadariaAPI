using PadariaAPI.DTO;
using System;

namespace PadariaAPI.Validate
{
    //classe ProdutoValidate implementa a validação dos dados do produto
    public class ProdutoValidate : IProdutoValidate
    {
        //método para validar um produto
        public bool Validar(ProdutoDTO produto)
        {
            //verifica se o produto é nulo
            if (produto == null)
                throw new ArgumentNullException("Produto não pode ser nulo.");

            //verifica se o nome do produto está vazio ou nulo
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ArgumentException("Nome do produto é obrigatório.");

            //valida o tamanho do nome do produto
            if (produto.Nome.Length < 3 || produto.Nome.Length > 100)
                throw new ArgumentException("Nome do produto deve ter entre 3 e 100 caracteres.");

            //verifica se o preço do produto é maior que zero
            if (produto.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");

            //verifica se a validade do produto não é anterior à data atual
            if (produto.Validade < DateTime.Today)
                throw new ArgumentException("A validade do produto não pode ser anterior à data atual.");

            //verifica se a unidade de medida está vazia ou nula
            if (string.IsNullOrWhiteSpace(produto.UnidadeMedida))
                throw new ArgumentException("Unidade de medida é obrigatória.");

            //valida se a unidade de medida está entre as opções permitidas
            var unidadesValidas = new[] { "KG", "UNIDADE", "LITRO" };
            if (!Array.Exists(unidadesValidas, u => u.Equals(produto.UnidadeMedida, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Unidade de medida inválida. As opções válidas são: KG, UNIDADE, LITRO.");

            return true; 
        }
    }
}
