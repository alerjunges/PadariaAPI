using Microsoft.EntityFrameworkCore;
using PadariaAPI.Classes;

namespace PadariaAPI.Data
{
    //classe InMemoryDbContext que representa o contexto do banco de dados
    public class InMemoryDbContext : DbContext
    {
        //construtor da classe InMemoryDbContext que recebe opções de configuração do DbContext
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; } //armazena os produtos
        public DbSet<Venda> Vendas { get; set; } //armazena as vendas
        public DbSet<Funcionario> Funcionarios { get; set; } //armazena os funcionários
        public DbSet<Cliente> Clientes { get; set; } //armazena os clientes
        public DbSet<Fornecedor> Fornecedores { get; set; } //armazena os fornecedores
        public DbSet<Compra> Compras { get; set; } //armazena as compras
        public DbSet<Ingrediente> Ingredientes { get; set; } //armazena os ingredientes
        public DbSet<Log> Logs { get; set; } //armazena os logs

        //método para configurar o modelo do banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //chama o método base para garantir que as configurações padrão sejam aplicadas
            base.OnModelCreating(modelBuilder);
        }
    }
}
