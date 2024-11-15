using PadariaAPI.Classes;
using System.Collections.Generic;

namespace PadariaAPI.Repository
{
    public interface ILogRepository
    {
        //lista todos os logs
        IEnumerable<Log> ListarTodos();
        //adiciona um novo log
        void Adicionar(Log log); 
    }
}
