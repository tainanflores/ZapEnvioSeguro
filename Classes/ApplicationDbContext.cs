using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contato> Contatos { get; set; } // Define a tabela Contatos
    public DbSet<Mensagens> Mensagens { get; set; } // Define a tabela Mensagens
    public DbSet<MensagemEnviada> MensagemEnviada { get; set; } // Define a tabela Mensagem enviada
    public DbSet<Users> Users { get; set; }

    // String de conexão com o banco de dados SQL Server
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=mundodigital.ddns.net,4200;Database=ZapEnvioFacil;User Id=mundodigital;Password=135246;Encrypt=False;");
    }

    // Opcional: Personalizar o modelo de dados, se necessário
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Definir valor padrão para a coluna Sexo
        modelBuilder.Entity<Contato>()
            .Property(c => c.Sexo)
            .HasDefaultValue("O");

        // Definir valor padrão para a coluna IsBusiness
        modelBuilder.Entity<Contato>()
            .Property(c => c.IsBusiness)
            .HasDefaultValue(false);
        
        modelBuilder.Entity<Mensagens>()
            .Property(c => c.QuantidadeContatosSucesso)
            .HasDefaultValue(0);
    }
}
