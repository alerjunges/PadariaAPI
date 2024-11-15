using PadariaAPI.Classes;
using PadariaAPI.Data;
using PadariaAPI.DTO;
using PadariaAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Services
{
    //classe LogService é responsável por controlar as regras de negócios
    public class LogService : ILogService
    {
        private readonly LogRepository _logRepository; //repositório para acessar os dados de logs

        //construtor recebe o contexto do banco e inicializa o repositório
        public LogService(InMemoryDbContext context)
        {
            _logRepository = new LogRepository(context); 
        }

        //método para listar todos os logs
        public IEnumerable<LogDTO> ListarTodos()
        {
            //mapeia os logs armazenados no banco para DTOs e retorna
            return _logRepository.ListarTodos().Select(log => new LogDTO
            {
                Id = log.Id,
                Acao = log.Acao,
                Usuario = log.Usuario,
                DataHora = log.DataHora
            });
        }

        //método para adicionar um novo log
        public LogDTO Adicionar(LogDTO logDto)
        {
            //cria uma entidade Log a partir do DTO
            var log = new Log
            {
                Acao = logDto.Acao,
                Usuario = logDto.Usuario,
                DataHora = logDto.DataHora
            };

            _logRepository.Adicionar(log); 
            logDto.Id = log.Id; 
            return logDto; 
        }
    }
}
