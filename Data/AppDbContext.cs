using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Models;

namespace EstoqueAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>()
            .HasIndex(c => c.Nome)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}


