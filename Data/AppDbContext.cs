using Microsoft.EntityFrameworkCore;
using ProjetoUNIVESP.Estudantes;

namespace ProjetoUNIVESP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EstudanteMap> Estudantes { get; set; }
        public DbSet<MentoriaMap> Mentoria { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;database=UNIVESP;user=root;password=7@Onlygo;",
                                        new MySqlServerVersion(new Version(8, 0, 36)));
            }
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
