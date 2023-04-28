using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Name = "Jesielo Manuelito",
                    Email = "Manuelito-bandeira@email.com",
                    Age = 25
                },
                new Aluno
                {
                    Id = 2,
                    Name = "Jesiela Manuelita",
                    Email = "Manuelita-bandeira@email.com",
                    Age = 50
                }
            );
        }
    }
}
