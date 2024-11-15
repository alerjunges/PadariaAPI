using PadariaAPI.Classes;
using PadariaAPI.Data;
using PadariaAPI.DTO;
using PadariaAPI.Repository;
using PadariaAPI.Validate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Services
{
    //classe IngredienteService é responsável por controlar as regras de negócios
    public class IngredienteService : IIngredienteService
    {
        private readonly IngredienteRepository _ingredienteRepository; //repositório para acessar os dados de ingredientes
        private readonly IngredienteValidate _ingredienteValidate; //validador para verificar os dados do ingrediente

        //construtor recebe o contexto do banco e inicializa o repositório e o validador
        public IngredienteService(InMemoryDbContext context)
        {
            _ingredienteRepository = new IngredienteRepository(context);
            _ingredienteValidate = new IngredienteValidate();
        }

        //método para buscar um ingrediente pelo id
        public IngredienteDTO ObterPorId(int id)
        {
            var ingrediente = _ingredienteRepository.ObterPorId(id); //busca o ingrediente no repositóri
            if (ingrediente == null)
                throw new ArgumentException("Ingrediente não encontrado."); //exceção se o ingrediente não existir

            //retorna um DTO com os dados do ingrediente
            return new IngredienteDTO
            {
                Id = ingrediente.Id,
                Nome = ingrediente.Nome,
                Preco = ingrediente.Preco,
                Validade = ingrediente.Validade,
                UnidadeMedida = ingrediente.UnidadeMedida
            };
        }

        //método para listar todos os ingredientes
        public IEnumerable<IngredienteDTO> ListarTodos()
        {
            //retorna um DTO com os dados do ingrediente
            return _ingredienteRepository.ListarTodos().Select(ingrediente => new IngredienteDTO
            {
                Id = ingrediente.Id,
                Nome = ingrediente.Nome,
                Preco = ingrediente.Preco,
                Validade = ingrediente.Validade,
                UnidadeMedida = ingrediente.UnidadeMedida
            });
        }

        //método para adicionar um ingrediente
        public IngredienteDTO Adicionar(IngredienteDTO ingredienteDto)
        {
            //valida os dados do ingrediente
            if (!_ingredienteValidate.Validar(ingredienteDto))
                throw new ArgumentException("Dados inválidos para o ingrediente."); //exceção se a validação falhar

            //cria uma entidade Ingrediente a partir do DTO
            var ingrediente = new Ingrediente
            {
                Nome = ingredienteDto.Nome,
                Preco = ingredienteDto.Preco,
                Validade = ingredienteDto.Validade,
                UnidadeMedida = ingredienteDto.UnidadeMedida
            };

            _ingredienteRepository.Adicionar(ingrediente);
            ingredienteDto.Id = ingrediente.Id;
            return ingredienteDto; 
        }

        //método para atualizar um ingrediente
        public void Atualizar(int id, IngredienteDTO ingredienteDto)
        {
            var ingredienteExistente = _ingredienteRepository.ObterPorId(id); //busca o ingrediente no repositóri
            if (ingredienteExistente == null)
                throw new ArgumentException("Ingrediente não encontrado."); //exceção se o ingrediente não existir

            //valida os dados do ingrediente
            if (!_ingredienteValidate.Validar(ingredienteDto))
                throw new ArgumentException("Dados inválidos para o ingrediente."); //exceção se a validação falhar

            //atualiza os dados do ingrediente
            ingredienteExistente.Nome = ingredienteDto.Nome;
            ingredienteExistente.Preco = ingredienteDto.Preco;
            ingredienteExistente.Validade = ingredienteDto.Validade;
            ingredienteExistente.UnidadeMedida = ingredienteDto.UnidadeMedida;

            _ingredienteRepository.Atualizar(ingredienteExistente);
        }

        //método para remover um ingrediente
        public void Remover(int id)
        {
            var ingrediente = _ingredienteRepository.ObterPorId(id); //busca o ingrediente no repositório
            if (ingrediente == null)
                throw new ArgumentException("Ingrediente não encontrado."); //exceção se o ingrediente não existir

            _ingredienteRepository.Remover(ingrediente);
        }
    }
}
