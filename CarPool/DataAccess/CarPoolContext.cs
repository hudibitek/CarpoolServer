using CarPool.DataAccess.Entities;
using CarPool.DataAccess.Faker;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CarPoolContext : DbContext
    {
        public CarPoolContext(DbContextOptions<CarPoolContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<TravelPlan> TravelPlans { get; set; }
        public virtual DbSet<TravelPlanEmployee> TravelPlanEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(b =>
            {                
                b.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Car>(b =>
            {
                b.HasIndex(e => e.Plates).IsUnique();
            });

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            //var faker = new CarPoolFaker();
            //modelBuilder.Entity<Employee>().HasData(faker.GenerateEmployees());
            //modelBuilder.Entity<Car>().HasData(faker.GenerateCars());
        }
    }
}
