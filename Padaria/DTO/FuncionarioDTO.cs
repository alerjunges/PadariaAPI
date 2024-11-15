using System;

namespace PadariaAPI.DTO
{
    public class FuncionarioDTO
    {
        //identificador único do funcionário
        public int Id { get; set; }

        //nome do funcionário
        public string Nome { get; set; }

        //cargo do funcionário
        public string Cargo { get; set; }

        //salário do funcionário
        public decimal Salario { get; set; }

        //data de admissão do funcionário 
        public DateTime DataAdmissao { get; set; }
    }
}
