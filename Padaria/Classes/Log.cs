namespace PadariaAPI.Classes
{
    public class Log
    {
        //identificador único do log
        public int Id { get; set; }
        //descrição da ação realizada
        public string Acao { get; set; }
        //nome do usuário que realizou a ação
        public string Usuario { get; set; }
        //data e hora da ação
        public DateTime DataHora { get; set; } 
    }
}
