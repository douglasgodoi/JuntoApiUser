using JuntoApi.Data.Maps;
using JuntoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JuntoApi.Data
{
    public class StoreDataContext: DbContext
    {
        public StoreDataContext()
        {

        }

        public StoreDataContext(DbContextOptions<StoreDataContext> options)
            : base(options)
        { 

        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=JuntoDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfiguration(new UserMap());
    }
}