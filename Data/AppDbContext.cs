namespace ApiDockerPiaget.Data
{
    using ApiDockerPiaget.Models;
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using Microsoft.EntityFrameworkCore;

    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Escola)
                .WithMany(e => e.Alunos)
                .HasForeignKey(a => a.EscolaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Professor>()
                .HasOne(p => p.Escola)
                .WithMany(e => e.Professores)
                .HasForeignKey(p => p.EscolaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
