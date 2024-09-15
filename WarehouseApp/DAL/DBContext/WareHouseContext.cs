using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DBContext
{
    public class WareHouseContext : DbContext
    {
        public WareHouseContext(DbContextOptions<WareHouseContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Users Table
            modelBuilder.Entity<Users>()
                .HasKey(u => u.Id); 

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email) 
                .IsUnique();

            modelBuilder.Entity<Users>()
                .Property(u => u.Name)
                .IsRequired(); 

            modelBuilder.Entity<Users>()
                .Property(u => u.Email)
                .IsRequired(); 

            modelBuilder.Entity<Users>()
                .Property(u => u.Role)
                .IsRequired(); 

            modelBuilder.Entity<Users>()
                .Property(u => u.Status)
                .IsRequired();

            modelBuilder.Entity<Users>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<Users>()
                .Property(u => u.CreatedDate)
                 .HasDefaultValueSql("GETDATE()"); // to add it auomatically in the DB without sending it with the request.


            //  Warehouse Table
            modelBuilder.Entity<Warehouse>()
                .HasKey(w => w.Id); 

            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.Name) 
                .IsUnique();

            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Name)
                .IsRequired(); 

            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Address)
                .IsRequired(); 

            modelBuilder.Entity<Warehouse>()
                .Property(w => w.City)
                .IsRequired(); 

            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Country)
                .IsRequired(); 

            // explain why i choose this relation because each user can have multiple warehouses .
            // but until now the warehouses should to be for one user only .
            // so i choose to make it one to many .
            // Note : i make it cascading to prevent the warehouses still without users : 
            // (in case we have a warehouses under specific user and we delete this user).
            // so , in this case we will remove all warehouses that found under this user .
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.User)  
                .WithMany()           
                .HasForeignKey(w => w.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .Property(u => u.CreatedDate)
                 .HasDefaultValueSql("GETDATE()");


            // Item Table
            modelBuilder.Entity<Item>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.Name) 
                .IsUnique();

            modelBuilder.Entity<Item>()
                .Property(i => i.Name)
                .IsRequired(); 

            modelBuilder.Entity<Item>()
                .Property(i => i.Quantity)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(i => i.Cost)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(i => i.SKUCode)
                .IsRequired(false);

            modelBuilder.Entity<Item>()
                .Property(i => i.MSRPPrice)
                .IsRequired(false); 

            // the same note that found for warehouse table for the relation .
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Warehouse)  
                .WithMany()               
                .HasForeignKey(i => i.WarehouseId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .Property(u => u.CreatedDate)
             .HasDefaultValueSql("GETDATE()");
        }

    }
}
