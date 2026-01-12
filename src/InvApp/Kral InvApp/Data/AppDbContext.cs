using Microsoft.EntityFrameworkCore;
using Kral_InvApp.Entities;

namespace Kral_InvApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(
                    "server=mysqlstudenti.litv.sssvt.cz;database=4c2_kralmatyas_db2;user=kralmatyas;password=123456"
                    
                );
            }
        }

        // ⬇⬇⬇ TOTO JE KLÍČ ⬇⬇⬇
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}

