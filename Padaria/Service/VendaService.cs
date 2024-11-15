using PadariaAPI.DTO;
using PadariaAPI.Classes;
using PadariaAPI.Repository;
using PadariaAPI.Validate;
using PadariaAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Services
{
    //classe VendaService é responsável por controlar as regras de negócios
    public class VendaService : IVendaService
    {
        private readonly VendaRepository _vendaRepository; //repositório para acessar os dados de vendas
        private readonly ProdutoRepository _produtoRepository; //repositório para acessar os dados de produtos
        private readonly FuncionarioRepository _funcionarioRepository; //repositório para acessar os dados de funcionários
        private readonly ClienteRepository _clienteRepository; //repositório para acessar os dados de clientes
        private readonly VendaValidate _vendaValidate; //validador para verificar os dados da venda

        //construtor recebe o contexto do banco e inicializa os repositórios e o validador
        public VendaService(InMemoryDbContext context)
        {
            _vendaRepository = new VendaRepository(context);
            _produtoRepository = new ProdutoRepository(context);
            _funcionarioRepository = new FuncionarioRepository(context);
            _clienteRepository = new ClienteRepository(context);
            _vendaValidate = new VendaValidate();
        }

        //implementação do método ObterVendaPorId definido na interface
        public VendaDTO ObterVendaPorId(int id)
        {
            var venda = _vendaRepository.ObterPorId(id); //busca a venda no repositório
            if (venda == null)
                throw new ArgumentException("Venda não encontrada."); //exceção se a venda não existir

            //mapeia os dados da venda para um DTO e retorna
            return new VendaDTO
            {
                Id = venda.Id,
                ProdutoIds = venda.Produtos.Select(p => p.Id).ToList(),
                ProdutosDetalhados = venda.Produtos.Select(p => new ProdutoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Validade = p.Validade,
                    UnidadeMedida = p.UnidadeMedida
                }).ToList(),
                DataVenda = venda.DataVenda,
                FuncionarioId = venda.Funcionario?.Id,
                FuncionarioNome = venda.Funcionario?.Nome,
                ClienteId = venda.Cliente?.Id,
                ClienteNome = venda.Cliente?.Nome,
                Total = CalcularTotal(venda) //calcula o total da venda com possíveis descontos
            };
        }

        //implementação do método ListarTodas definido na interface
        public IEnumerable<VendaDTO> ListarTodas()
        {
            var vendas = _vendaRepository.ListarTodas(); //busca todas as vendas no repositório

            //mapeia as vendas para DTOs e retorna
            return vendas.Select(venda => new VendaDTO
            {
                Id = venda.Id,
                ProdutoIds = venda.Produtos.Select(p => p.Id).ToList(),
                ProdutosDetalhados = venda.Produtos.Select(p => new ProdutoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Validade = p.Validade,
                    UnidadeMedida = p.UnidadeMedida
                }).ToList(),
                DataVenda = venda.DataVenda,
                FuncionarioId = venda.Funcionario?.Id,
                FuncionarioNome = venda.Funcionario?.Nome,
                ClienteId = venda.Cliente?.Id,
                ClienteNome = venda.Cliente?.Nome,
                Total = CalcularTotal(venda)
            });
        }

        //implementação do método RegistrarVenda definido na interface
        public void RegistrarVenda(VendaDTO vendaDto)
        {
            //busca os produtos pelos IDs
            var produtos = _produtoRepository.ListarTodos()
                .Where(p => vendaDto.ProdutoIds.Contains(p.Id))
                .ToList();

            if (!produtos.Any()) //verifica se nenhum produto foi encontrado
                throw new ArgumentException("Nenhum produto encontrado para a venda.");

            var funcionario = _funcionarioRepository.ObterPorId(vendaDto.FuncionarioId ?? 0);
            if (funcionario == null)
                throw new ArgumentException("Funcionário não encontrado."); //exceção se o funcionário não existir

            var cliente = _clienteRepository.ObterPorId(vendaDto.ClienteId ?? 0);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado."); //exceção se o cliente não existir

            //cria uma nova entidade Venda
            var venda = new Venda
            {
                Produtos = produtos,
                DataVenda = vendaDto.DataVenda,
                Funcionario = funcionario,
                Cliente = cliente
            };

            if (!_vendaValidate.Validar(venda)) //valida os dados da venda
                throw new ArgumentException("Dados inválidos para a venda.");

            _vendaRepository.Adicionar(venda);
        }

        //implementação do método AtualizarVenda definido na interface
        public void AtualizarVenda(int id, VendaDTO vendaDto)
        {
            var venda = _vendaRepository.ObterPorId(id); //busca a venda no repositório
            if (venda == null)
                throw new ArgumentException("Venda não encontrada."); //exceção se a venda não existir

            //atualiza os dados da venda
            venda.Produtos = _produtoRepository.ListarTodos()
                .Where(p => vendaDto.ProdutoIds.Contains(p.Id))
                .ToList();
            venda.DataVenda = vendaDto.DataVenda;
            venda.Funcionario = _funcionarioRepository.ObterPorId(vendaDto.FuncionarioId ?? 0);
            venda.Cliente = _clienteRepository.ObterPorId(vendaDto.ClienteId ?? 0);

            if (!_vendaValidate.Validar(venda))
                throw new ArgumentException("Dados inválidos para atualização da venda.");

            _vendaRepository.Atualizar(venda);
        }

        //implementação do método RemoverVenda definido na interface
        public void RemoverVenda(int id)
        {
            var venda = _vendaRepository.ObterPorId(id); //busca a venda no repositório
            if (venda == null)
                throw new ArgumentException("Venda não encontrada."); //exceção se a venda não existir

            _vendaRepository.Remover(venda);
        }

        //método para calcular o total da venda
        private decimal CalcularTotal(Venda venda)
        {
            //aplica um desconto de 50% para produtos que estão vencendo hoje
            return venda.Produtos.Sum(p => p.Validade.Date == venda.DataVenda.Date ? p.Preco * 0.5m : p.Preco);
        }
    }
}
