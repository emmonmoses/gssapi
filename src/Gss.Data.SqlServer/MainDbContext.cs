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

        // public MarkDbContext(string connectionString) : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        // {

        // }

        // declaration is necessary if GetRecords uses Option 1
        public virtual DbSet<Student>? Students { get; set; }
        public virtual DbSet<User>? Users { get; set; }

        
        public DbSet<T> GetRecords<T>() where T : class
        {
            /// <remarks>
            /// Option 1
            /// returns DbSet<T>
            /// </remarks>
            return this.Set<T>();

            /// <remarks>
            /// Option 2
            /// returns IQueryable<T>
            /// and we have no access to DbSet methods e.g. .Add() or .Remove()
            /// </remarks>
            // var dictionary = new Dictionary<string, Func<MarkDbContext, IQueryable<T>>>()
            // {
            //     { _tableName, (MarkDbContext context ) => context.Set<T>() }
            // };

            // var dbSet = dictionary[_tableName].Invoke(this);
            
            // return type is IQueryable<T>
            // but we have no access to DbSet methods e.g. .Add() or .Remove()
            // return dbSet;
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
            modelBuilder.Entity<Student>().ToTable($"tbl_student");
            modelBuilder.Entity<User>().ToTable($"tbl_user");
        }
    }
}