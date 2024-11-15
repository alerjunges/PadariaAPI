using System;

namespace PadariaAPI.Classes
{
    public class Funcionario
    {
        //armazena o identificador único do funcionário
        public int Id { get; set; }

        //armazena o nome do funcionário
        public string Nome { get; set; }

        //indica o papel dele na empresa
        public string Cargo { get; set; }

        //armazena o salário do funcionário
        public decimal Salario { get; set; }

        //data de admissão do funcionário
        //DateTime.Today para registrar a data atual
        public DateTime DataAdmissao { get; set; } = DateTime.Today;

        //tempo de serviço do funcionário em anos
        public int CalcularTempoDeServico()
        {
            return (int)((DateTime.Today - DataAdmissao).TotalDays / 365);
        }

        //calcula o salário anual do funcionário
        public decimal CalcularSalarioAnual()
        {
            return Salario * 12;
        }
    }
}
