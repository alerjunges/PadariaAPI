using PadariaAPI.Classes;
using System;

namespace PadariaAPI.Validate
{
    //classe VendaValidate implementa a validação dos dados da venda
    public class VendaValidate : IVendaValidate
    {
        //método para validar uma venda
        public bool Validar(Venda venda)
        {
            //verifica se a venda é nula
            if (venda == null)
                throw new ArgumentNullException("Venda não pode ser nula.");

            //verifica se a lista de produtos está vazia ou nula
            if (venda.Produtos == null || !venda.Produtos.Any())
                throw new ArgumentException("A venda deve conter pelo menos um produto.");

            //valida se a data da venda foi preenchida
            if (venda.DataVenda == default)
                throw new ArgumentException("A data da venda é obrigatória e deve ser válida.");

            //valida se a data da venda não está no futuro
            if (venda.DataVenda > DateTime.Now)
                throw new ArgumentException("A data da venda não pode ser no futuro.");

            //verifica se a venda está associada a um funcionário
            if (venda.Funcionario == null)
                throw new ArgumentException("A venda deve ser associada a um funcionário.");

            foreach (var produto in venda.Produtos)
            {
                //verifica se o preço do produto é válido
                if (produto.Preco <= 0)
                    throw new ArgumentException($"O produto '{produto.Nome}' possui um preço inválido.");

                //verifica se o produto está vencido
                if (produto.Validade < DateTime.Today)
                    throw new ArgumentException($"O produto '{produto.Nome}' está vencido e não pode ser vendido.");
            }

            return true; 
        }
    }
}
