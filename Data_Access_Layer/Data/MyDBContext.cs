using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Data
{
    public class MyDBContext : IdentityDbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<Court> Court { get; set; }

        public DbSet<Booking> Booking { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Vote> Vote { get; set; }

        public DbSet<ProductOrder> ProductOrder { get; set; }

        public DbSet<Bill> Bill { get; set; }

        public DbSet<ProductBill> ProductBill { get; set; }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "4b4c89bf-5bec-446d-a9a1-299028514748";
            var adminRoleId = "9a028c29-21c0-49b5-9458-258ca1f0341e";
            var employeeRoleId = "D590453B-9465-4176-AF51-086C66C15BFB";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                 new IdentityRole
                {
                    Id = employeeRoleId,
                    ConcurrencyStamp = employeeRoleId,
                    Name = "Employee",
                    NormalizedName = "Employee".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
