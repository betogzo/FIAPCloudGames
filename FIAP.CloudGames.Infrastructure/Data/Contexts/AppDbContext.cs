using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FIAP.CloudGames.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUsuario(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureUsuario(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            
            entity.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedNever();

            entity.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.Email)
                .HasConversion(
                    email => email.Endereco,
                    endereco => Email.Create(endereco).Data)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Email");

            entity.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Usuarios_Email");

            entity.Property(u => u.Senha)
                .HasConversion(
                    senha => senha.ValorHash,
                    hash => Senha.Create(hash).Data)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("SenhaHash");

            entity.Property(u => u.Tipo)
                .HasConversion<int>()
                .IsRequired()
                .HasColumnName("TipoUsuario");

            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(u => u.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            entity.ToTable("Usuarios");
        });
    }
    
    
    
    
}