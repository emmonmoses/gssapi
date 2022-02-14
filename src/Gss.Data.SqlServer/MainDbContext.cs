using Gss.Models;
using Microsoft.EntityFrameworkCore;

namespace Gss.Data.SqlServer
{
    public class MainDbContext : DbContext
    {
        private readonly string _connectionString;

        public MainDbContext()
        {
            var dbOptions = DbOptionsHelper.GetDatabaseOptions();
            _connectionString = $"data source={dbOptions.Server};initial catalog=gssmandb;User Id={dbOptions.UserId};Password={dbOptions.Password};integrated security={dbOptions.IntegratedSecurity};MultipleActiveResultSets={dbOptions.MultipleActiveResultSets};";
        }       
        public virtual DbSet<Student>? Students { get; set; }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<UserDetail>? UserDetails { get; set; }
        public virtual DbSet<Branch>? Branches { get; set; }
        public virtual DbSet<Registration>? Registrations { get; set; }
        public virtual DbSet<StudentAge>? StudentAges { get; set; }
        public virtual DbSet<StudentDetail>? StudentDetails { get; set; }
        public virtual DbSet<StudentExtraDetail>? StudentExtraDetails { get; set; }
        public virtual DbSet<StudentExtraDetailMore>? StudentExtraDetailMores { get; set; }
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
            
            modelBuilder.Entity<Student>().ToTable($"tbl_student");
            modelBuilder.Entity<User>().ToTable($"tbl_user");
            modelBuilder.Entity<UserDetail>().ToTable($"userdetail");
            modelBuilder.Entity<Branch>().ToTable($"branches");
            modelBuilder.Entity<Registration>().ToTable($"registrations");
            modelBuilder.Entity<StudentAge>().ToTable($"tbl_studentages");
            modelBuilder.Entity<StudentDetail>().ToTable($"tbl_studentdetail");
            modelBuilder.Entity<StudentExtraDetail>().ToTable($"tbl_studentexdetail");
            modelBuilder.Entity<StudentExtraDetailMore>().ToTable($"tbl_studentexdetailmore");
        }
    }
}