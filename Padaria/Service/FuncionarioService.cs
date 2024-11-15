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
    //classe FuncionarioService é responsável por controlar as regras de negócios
    public class FuncionarioService : IFuncionarioService
    {
        private readonly FuncionarioRepository _funcionarioRepository; //repositório para acessar os dados dos funcionários
        private readonly FuncionarioValidate _funcionarioValidate; //validador para verificar os dados do funcionário

        //construtor recebe o contexto do banco e inicializa o repositório e o validador
        public FuncionarioService(InMemoryDbContext context)
        {
            _funcionarioRepository = new FuncionarioRepository(context);
            _funcionarioValidate = new FuncionarioValidate();
        }

        //método para buscar um funcionário pelo id
        public FuncionarioDTO ObterPorId(int id)
        {
            var funcionario = _funcionarioRepository.ObterPorId(id); //busca o funcionário no repositório
            if (funcionario == null)
                throw new ArgumentException("Funcionário não encontrado."); //exceção se o funcionário não existir

            //retorna um DTO com os dados do funcionário
            return new FuncionarioDTO
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Cargo = funcionario.Cargo,
                Salario = funcionario.Salario,
                DataAdmissao = funcionario.DataAdmissao
            };
        }

        //método para listar todos os funcionários
        public IEnumerable<FuncionarioDTO> ListarTodos()
        {
            //mapeia os dados dos funcionários para DTOs e retorna
            return _funcionarioRepository.ListarTodos().Select(funcionario => new FuncionarioDTO
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Cargo = funcionario.Cargo,
                Salario = funcionario.Salario,
                DataAdmissao = funcionario.DataAdmissao
            });
        }

        //método para adicionar um novo funcionário
        public FuncionarioDTO Adicionar(FuncionarioDTO funcionarioDto)
        {
            //valida os dados do funcionário
            if (!_funcionarioValidate.Validar(funcionarioDto))
                throw new ArgumentException("Dados inválidos para o funcionário."); //exceção se a validação falhar

            //cria uma entidade Funcionario a partir do DTO
            var funcionario = new Funcionario
            {
                Nome = funcionarioDto.Nome,
                Cargo = funcionarioDto.Cargo,
                Salario = funcionarioDto.Salario,
                DataAdmissao = funcionarioDto.DataAdmissao
            };

            _funcionarioRepository.Adicionar(funcionario);
            funcionarioDto.Id = funcionario.Id;
            return funcionarioDto;
        }

        //método para atualizar um funcionário
        public void Atualizar(int id, FuncionarioDTO funcionarioDto)
        {
            var funcionarioExistente = _funcionarioRepository.ObterPorId(id); //busca o funcionário no repositório
            if (funcionarioExistente == null)
                throw new ArgumentException("Funcionário não encontrado."); //exceção se o funcionário não existir

            //valida os dados do funcionário
            if (!_funcionarioValidate.Validar(funcionarioDto))
                throw new ArgumentException("Dados inválidos para o funcionário."); //exceção se a validação falhar

            //atualiza os dados do funcionário
            funcionarioExistente.Nome = funcionarioDto.Nome;
            funcionarioExistente.Cargo = funcionarioDto.Cargo;
            funcionarioExistente.Salario = funcionarioDto.Salario;
            funcionarioExistente.DataAdmissao = funcionarioDto.DataAdmissao;

            _funcionarioRepository.Atualizar(funcionarioExistente);
        }

        //método para remover um funcionário
        public void Remover(int id)
        {
            var funcionario = _funcionarioRepository.ObterPorId(id); //busca o funcionário no repositório
            if (funcionario == null)
                throw new ArgumentException("Funcionário não encontrado."); //exceção se o funcionário não existir

            _funcionarioRepository.Remover(funcionario);
        }
    }
}
