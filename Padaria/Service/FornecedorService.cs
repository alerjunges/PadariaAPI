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
    //classe FornecedorService é responsável por controlar as regras de negócios
    public class FornecedorService : IFornecedorService
    {
        private readonly FornecedorRepository _fornecedorRepository; //repositório para acessar os dados dos fornecedores
        private readonly FornecedorValidate _fornecedorValidate; //validador para verificar os dados do fornecedor

        //construtor recebe o contexto do banco e inicializa o repositório e o validador
        public FornecedorService(InMemoryDbContext context)
        {
            _fornecedorRepository = new FornecedorRepository(context);
            _fornecedorValidate = new FornecedorValidate();
        }

        //método para buscar um fornecedor pelo id
        public FornecedorDTO ObterPorId(int id)
        {
            var fornecedor = _fornecedorRepository.ObterPorId(id); //busca o fornecedor no repositório
            if (fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado."); //exceção se o fornecedor não existir

            //retorna um DTO com os dados do fornecedor 
            return new FornecedorDTO
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Email = fornecedor.Email,
                Telefone = fornecedor.Telefone,
                DataCadastro = fornecedor.DataCadastro
            };
        }

        //método para listar todos os fornecedores
        public IEnumerable<FornecedorDTO> ListarTodos()
        {
            //retorna um DTO com os dados do fornecedor
            return _fornecedorRepository.ListarTodos().Select(fornecedor => new FornecedorDTO
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Email = fornecedor.Email,
                Telefone = fornecedor.Telefone,
                DataCadastro = fornecedor.DataCadastro
            });
        }

        //método para adicionar um novo fornecedor
        public FornecedorDTO Adicionar(FornecedorDTO fornecedorDto)
        {
            //valida os dados do fornecedor
            if (!_fornecedorValidate.Validar(fornecedorDto))
                throw new ArgumentException("Dados inválidos para o fornecedor."); //exceção se a validação falhar

            //cria uma entidade Fornecedor a partir do DTO
            var fornecedor = new Fornecedor
            {
                Nome = fornecedorDto.Nome,
                Email = fornecedorDto.Email,
                Telefone = fornecedorDto.Telefone,
                DataCadastro = fornecedorDto.DataCadastro
            };

            _fornecedorRepository.Adicionar(fornecedor);
            fornecedorDto.Id = fornecedor.Id;
            return fornecedorDto;
        }

        //método para atualizar um fornecedor 
        public void Atualizar(int id, FornecedorDTO fornecedorDto)
        {
            var fornecedorExistente = _fornecedorRepository.ObterPorId(id); //busca o fornecedor no repositório
            if (fornecedorExistente == null)
                throw new ArgumentException("Fornecedor não encontrado."); //exceção se o fornecedor não existir

            //valida os dados do fornecedor
            if (!_fornecedorValidate.Validar(fornecedorDto))
                throw new ArgumentException("Dados inválidos para o fornecedor."); //exceção se a validação falhar

            //atualiza os dados do fornecedor 
            fornecedorExistente.Nome = fornecedorDto.Nome;
            fornecedorExistente.Email = fornecedorDto.Email;
            fornecedorExistente.Telefone = fornecedorDto.Telefone;
            fornecedorExistente.DataCadastro = fornecedorDto.DataCadastro;

            _fornecedorRepository.Atualizar(fornecedorExistente);
        }

        //método para remover um fornecedor
        public void Remover(int id)
        {
            var fornecedor = _fornecedorRepository.ObterPorId(id); //busca o fornecedor no repositório
            if (fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado."); //exceção se o fornecedor não existir

            _fornecedorRepository.Remover(fornecedor);
        }
    }
}
