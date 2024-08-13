using JaiminPatelECommerce.Configurations;
using JaiminPatelECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JaiminPatelECommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for each entity in your application
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations using Fluent API
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            // Seed initial data
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedProducts(modelBuilder);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = "1",
                UserName = "admin@ecommerce.com",
                NormalizedUserName = "ADMIN@ECOMMERCE.COM",
                Email = "admin@ecommerce.com",
                NormalizedEmail = "ADMIN@ECOMMERCE.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User"
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });
        }

        private void SeedProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Apple iPhone 13 Pro",
                    Description = "The iPhone 13 Pro features a 6.1-inch Super Retina XDR display with ProMotion for a faster, more responsive feel. The new Ultra Wide camera reveals more detail in the dark areas of your photos, while the Wide camera captures 2.2x more light for better photos and videos. The telephoto camera now has 3x optical zoom. The iPhone 13 Pro also introduces Cinematic mode, allowing you to create movies with shallow depth of field and automatic focus changes.",
                    Price = 999.99m,
                    Stock = 25,
                    ImageUrl = "https://m.media-amazon.com/images/I/81qlME2wWmL._SL1500_.jpg",
                    IsAvailable = true
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Apple MacBook Pro 14-inch",
                    Description = "The MacBook Pro 14-inch is powered by the Apple M3 Pro chip, delivering up to 11-core CPU and up to 16-core GPU performance. It features a stunning 14-inch Liquid Retina XDR display, with extreme dynamic range and contrast ratio. It also offers up to 32GB of unified memory and up to 8TB of superfast SSD storage, making it a powerhouse for professionals who need high performance for demanding tasks. The new MacBook Pro also features a 1080p FaceTime HD camera, a six-speaker sound system, and studio-quality mics.",
                    Price = 1999.99m,
                    Stock = 15,
                    ImageUrl = "https://m.media-amazon.com/images/I/61RJn0ofUsL._SL1500_.jpg",
                    IsAvailable = true
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Apple iPad Pro 13-inch",
                    Description = "The iPad Pro 13-inch is equipped with the powerful Apple M4 chip, offering incredible performance and all-day battery life. The Liquid Retina XDR display delivers extreme dynamic range and an immersive viewing experience. With 5G capability, you can stay connected at lightning speeds. The iPad Pro also features advanced pro cameras, a LiDAR scanner, and support for the Apple Pencil (2nd generation) and the Magic Keyboard, making it the ultimate tool for creativity and productivity.",
                    Price = 1099.99m,
                    Stock = 20,
                    ImageUrl = "https://m.media-amazon.com/images/I/7131GFmmnCL._SL1500_.jpg",
                    IsAvailable = true
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Apple AirPods Pro",
                    Description = "The AirPods Pro feature Active Noise Cancellation for immersive sound. Transparency mode for hearing and connecting with the world around you. They offer a customizable fit for all-day comfort, are sweat and water-resistant, and include a wireless charging case that provides more than 24 hours of battery life. The AirPods Pro are the perfect choice for music lovers and anyone who wants to enjoy superior sound quality on the go.",
                    Price = 249.99m,
                    Stock = 50,
                    ImageUrl = "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MTJV3?wid=1144&hei=1144&fmt=jpeg&qlt=95&.v=1694014871985",
                    IsAvailable = true
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Apple Watch Series 9",
                    Description = "The Apple Watch Series 9 features the largest, most advanced display yet, making it easier to use and read. It is the most durable Apple Watch ever, with a crack-resistant front crystal. The Series 7 also offers powerful health features, such as blood oxygen measurement, ECG monitoring, and the ability to track your sleep. With new workout types and a redesigned interface, it's the ultimate fitness companion.",
                    Price = 399.99m,
                    Stock = 40,
                    ImageUrl = "https://m.media-amazon.com/images/I/8100NXybCEL._SL1500_.jpg",
                    IsAvailable = true
                }
            );

        }

        // Override SaveChangesAsync to add auditing or other logic
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Override SaveChanges for synchronous operations
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
