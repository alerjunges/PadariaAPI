using PadariaAPI.DTO;
using System.Collections.Generic;

namespace PadariaAPI.Services
{
    public interface ILogService
    {
        //lista todos os logs
        IEnumerable<LogDTO> ListarTodos();
        //adiciona um novo log
        LogDTO Adicionar(LogDTO logDto); 
    }
}
