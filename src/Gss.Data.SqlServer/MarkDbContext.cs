using Gss.Models;
using Microsoft.EntityFrameworkCore;

namespace Gss.Data.SqlServer
{
    public class MarkDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _academicyear;

        public MarkDbContext(string database, string academicyear)
        {
            var dbOptions = DbOptionsHelper.GetDatabaseOptions();

            _connectionString = $"data source={dbOptions.Server};initial catalog={database};User Id={dbOptions.UserId};Password={dbOptions.Password};integrated security={dbOptions.IntegratedSecurity};MultipleActiveResultSets={dbOptions.MultipleActiveResultSets};";
            _academicyear = academicyear;
        }

        // declaration is necessary if GetRecords uses Option 1
        public virtual DbSet<TestMark>? TestMarks { get; set; }
        public virtual DbSet<WeeklyMark>? WeeklyMarks { get; set; }


        public DbSet<T> GetRecords<T>() where T : class
        {
            return this.Set<T>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString,
                providerOptions => { providerOptions.EnableRetryOnFailure(); });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // dynamically map the table names based on academic year
            modelBuilder.Entity<TestMark>().ToTable($"testmarks{_academicyear}");
            modelBuilder.Entity<WeeklyMark>().ToTable($"weeklymarks{_academicyear}");
        }
    }
}