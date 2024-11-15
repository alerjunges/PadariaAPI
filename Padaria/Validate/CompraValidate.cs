using PadariaAPI.Classes;
using System;
using System.Linq;

namespace PadariaAPI.Validate
{
    //classe CompraValidate implementa a validação dos dados da compra
    public class CompraValidate : ICompraValidate
    {
        //método para validar uma compra
        public bool Validar(Compra compra)
        {
            //verifica se a compra é nula
            if (compra == null)
                throw new ArgumentNullException("Compra não pode ser nula.");

            //verifica se a lista de ingredientes está nula ou vazia
            if (compra.Ingredientes == null || !compra.Ingredientes.Any())
                throw new ArgumentException("A compra deve conter pelo menos um ingrediente.");

            //verifica se a data da compra foi preenchida corretamente
            if (compra.DataCompra == default)
                throw new ArgumentException("A data da compra é obrigatória e deve ser válida.");

            //verifica se a data da compra não está no futuro
            if (compra.DataCompra > DateTime.Now)
                throw new ArgumentException("A data da compra não pode ser no futuro.");

            //verifica se a compra está associada a um fornecedor
            if (compra.Fornecedor == null)
                throw new ArgumentException("A compra deve ser associada a um fornecedor.");

            //valida o preço de cada um
            foreach (var ingrediente in compra.Ingredientes)
            {
                //verifica se o preço do ingrediente é menor ou igual a zero
                if (ingrediente.Preco <= 0)
                    throw new ArgumentException($"O ingrediente '{ingrediente.Nome}' possui um preço inválido.");
            }

            return true; 
        }
    }
}
