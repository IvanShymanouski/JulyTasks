using System.Data.Entity;
using System.Diagnostics;

namespace ORM
{
    public class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
            Debug.WriteLine("Context create!");
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            
        }

        public new void Dispose()
        {
            base.Dispose();
            Debug.WriteLine("Dispose in context!");
        }
    }
}
