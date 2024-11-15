using PadariaAPI.Classes;
using PadariaAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace PadariaAPI.Repository
{
    //classe LogRepository implementa os métodos da interface ILogRepository
    public class LogRepository : ILogRepository
    {
        private readonly InMemoryDbContext _context; //contexto do banco de dados em memória

        //construtor recebe o contexto como dependência
        public LogRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        //método para listar todos os logs
        public IEnumerable<Log> ListarTodos()
        {
            return _context.Logs.ToList();
        }

        //método para adicionar um novo log
        public void Adicionar(Log log)
        {
            
            _context.Logs.Add(log);
            _context.SaveChanges(); 
        }
    }
}
