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
    //classe ClienteService é responsável por controlar as regras de negócios
    public class ClienteService : IClienteService
    {
        private readonly ClienteRepository _clienteRepository; //repositório para acessar os dados de clientes
        private readonly ClienteValidate _clienteValidate; //validador para verificar os dados do cliente

        //construtor recebe o contexto do banco e inicializa o repositório e o validador
        public ClienteService(InMemoryDbContext context)
        {
            _clienteRepository = new ClienteRepository(context); 
            _clienteValidate = new ClienteValidate(); 
        }

        //método para buscar um cliente pelo id
        public ClienteDTO ObterPorId(int id)
        {
            var cliente = _clienteRepository.ObterPorId(id); //busca o cliente no repositório
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado."); //exceção se o cliente não existir

            //retorna um DTO com os dados do cliente
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                DataCadastro = cliente.DataCadastro
            };
        }

        //método para listar todos os clientes
        public IEnumerable<ClienteDTO> ListarTodos()
        {
            //retorna um DTO com os dados do cliente
            return _clienteRepository.ListarTodos().Select(cliente => new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                DataCadastro = cliente.DataCadastro
            });
        }

        //método para adicionar um cliente
        public ClienteDTO Adicionar(ClienteDTO clienteDto)
        {
            // valida os dados do cliente
            if (!_clienteValidate.Validar(clienteDto))
                throw new ArgumentException("Dados inválidos para o cliente."); //exceção se a validação falhar

            //cria uma entidade Cliente a partir do DTO
            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                Telefone = clienteDto.Telefone,
                DataCadastro = clienteDto.DataCadastro
            };

            _clienteRepository.Adicionar(cliente); 
            clienteDto.Id = cliente.Id; 
            return clienteDto; 
        }

        //método para atualizar um cliente
        public void Atualizar(int id, ClienteDTO clienteDto)
        {
            var clienteExistente = _clienteRepository.ObterPorId(id); //busca o cliente no repositório
            if (clienteExistente == null)
                throw new ArgumentException("Cliente não encontrado."); //exceção se o cliente não existir

            //valida os dados do cliente
            if (!_clienteValidate.Validar(clienteDto))
                throw new ArgumentException("Dados inválidos para o cliente."); //exceção se a validação falhar

            //atualiza os dados do cliente 
            clienteExistente.Nome = clienteDto.Nome;
            clienteExistente.Email = clienteDto.Email;
            clienteExistente.Telefone = clienteDto.Telefone;
            clienteExistente.DataCadastro = clienteDto.DataCadastro;

            _clienteRepository.Atualizar(clienteExistente);
        }

        //método para remover um cliente
        public void Remover(int id)
        {
            var cliente = _clienteRepository.ObterPorId(id); //busca o cliente no repositório
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado."); //exceção se o cliente não existir

            _clienteRepository.Remover(cliente);
        }
    }
}
