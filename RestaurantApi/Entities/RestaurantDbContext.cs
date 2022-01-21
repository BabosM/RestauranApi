using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Entities
{
    public class RestaurantDbContext : DbContext
    {

        
        
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User> Roles { get; set; }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Dish>()
                .Property(r => r.Name)
                .IsRequired();
            modelBuilder.Entity<Address>()
                .Property(r => r.Street)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Address>()
            .Property(r => r.City)
            .IsRequired()
            .HasMaxLength(50);
            
        }
            
    }
}