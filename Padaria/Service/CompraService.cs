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
    //classe CompraService é responsável por controlar as regras de negócios 
    public class CompraService : ICompraService
    {
        private readonly CompraRepository _compraRepository; //repositório para acessar os dados de compras
        private readonly FornecedorRepository _fornecedorRepository; //repositório para acessar os dados de fornecedores
        private readonly FuncionarioRepository _funcionarioRepository; //repositório para acessar os dados de funcionários
        private readonly IngredienteRepository _ingredienteRepository; //repositório para acessar os dados de ingredientes
        private readonly CompraValidate _compraValidate; //validador para verificar os dados da compra

        //construtor recebe o contexto do banco e inicializa os repositórios e o validador
        public CompraService(InMemoryDbContext context)
        {
            _compraRepository = new CompraRepository(context);
            _fornecedorRepository = new FornecedorRepository(context);
            _funcionarioRepository = new FuncionarioRepository(context);
            _ingredienteRepository = new IngredienteRepository(context);
            _compraValidate = new CompraValidate();
        }

        //método para buscar uma compra pelo id
        public CompraDTO ObterCompraPorId(int id)
        {
            var compra = _compraRepository.ObterPorId(id); // busca a compra pelo ID
            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            // mapeia a compra para o DTO
            return new CompraDTO
            {
                Id = compra.Id,
                IngredienteIds = compra.Ingredientes?.Select(i => i.Id).ToList() ?? new List<int>(),
                IngredientesDetalhados = compra.Ingredientes?.Select(i => new IngredienteDTO
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Validade = i.Validade,
                    UnidadeMedida = i.UnidadeMedida
                }).ToList() ?? new List<IngredienteDTO>(),
                DataCompra = compra.DataCompra,
                FornecedorId = compra.Fornecedor?.Id ?? 0,
                FornecedorNome = compra.Fornecedor?.Nome ?? string.Empty,
                FuncionarioId = compra.Funcionario?.Id ?? 0,
                FuncionarioNome = compra.Funcionario?.Nome ?? string.Empty,
                Total = compra.Ingredientes?.Sum(i => i.Preco) ?? 0
            };
        }

        //método para listar todas as compras
        public IEnumerable<CompraDTO> ListarTodas()
        {
            var compras = _compraRepository.ListarTodas(); // busca todas as compras do repositório

            // mapeia cada compra para o DTO
            return compras.Select(compra => new CompraDTO
            {
                Id = compra.Id,
                IngredienteIds = compra.Ingredientes?.Select(i => i.Id).ToList() ?? new List<int>(), 
                IngredientesDetalhados = compra.Ingredientes?.Select(i => new IngredienteDTO
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Validade = i.Validade,
                    UnidadeMedida = i.UnidadeMedida
                }).ToList() ?? new List<IngredienteDTO>(), 
                DataCompra = compra.DataCompra,
                FornecedorId = compra.Fornecedor?.Id ?? 0,
                FornecedorNome = compra.Fornecedor?.Nome ?? string.Empty,
                FuncionarioId = compra.Funcionario?.Id ?? 0,
                FuncionarioNome = compra.Funcionario?.Nome ?? string.Empty,
                Total = compra.Ingredientes?.Sum(i => i.Preco) ?? 0 // calcula o total 
            });
        }

        //método para registrar uma nova compra
        public void RegistrarCompra(CompraDTO compraDto)
        {
            //busca os ingredientes pelos IDs fornecidos no DTO
            var ingredientes = _ingredienteRepository.ListarTodos()
                .Where(i => compraDto.IngredienteIds.Contains(i.Id))
                .ToList();

            if (!ingredientes.Any()) //verifica se nenhum ingrediente foi encontrado
                throw new ArgumentException("Nenhum ingrediente encontrado para a compra.");

            var fornecedor = _fornecedorRepository.ObterPorId(compraDto.FornecedorId ?? 0);
            if (fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado.");

            var funcionario = _funcionarioRepository.ObterPorId(compraDto.FuncionarioId ?? 0);
            if (funcionario == null)
                throw new ArgumentException("Funcionário não encontrado.");

            //cria uma nova entidade Compra
            var compra = new Compra
            {
                Ingredientes = ingredientes,
                DataCompra = compraDto.DataCompra,
                Fornecedor = fornecedor,
                Funcionario = funcionario
            };

            if (!_compraValidate.Validar(compra)) //valida os dados da compra
                throw new ArgumentException("Dados inválidos para a compra.");

            _compraRepository.Adicionar(compra);
        }

        //método para atualizar uma compra existente
        public void AtualizarCompra(CompraDTO compraDto)
        {
            var compra = _compraRepository.ObterPorId(compraDto.Id); //busca a compra no repositório
            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            //atualiza os dados da compra com base no DTO
            compra.Ingredientes = _ingredienteRepository.ListarTodos()
                .Where(i => compraDto.IngredienteIds.Contains(i.Id))
                .ToList();
            compra.DataCompra = compraDto.DataCompra;
            compra.Fornecedor = _fornecedorRepository.ObterPorId(compraDto.FornecedorId ?? 0);
            compra.Funcionario = _funcionarioRepository.ObterPorId(compraDto.FuncionarioId ?? 0);

            if (!_compraValidate.Validar(compra))
                throw new ArgumentException("Dados inválidos para atualização da compra.");

            _compraRepository.Atualizar(compra);
        }

        //método para remover uma compra
        public void RemoverCompra(int id)
        {
            var compra = _compraRepository.ObterPorId(id); //busca a compra no repositório
            if (compra == null)
                throw new ArgumentException("Compra não encontrada."); //exceção se a compra não existir

            _compraRepository.Remover(compra);
        }
    }
}
