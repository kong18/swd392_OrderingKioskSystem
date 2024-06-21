using OrderingKioskSystem.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;


namespace OrderingKioskSystem.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        var migrator = this.Database.GetService<IMigrator>();
        migrator.Migrate();
    }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    public DbSet<KioskEntity> Kiosk { get; set; }
    public DbSet<ShipperEntity> Shippers { get; set; }
    public DbSet<PaymentEntity> Payments { get; set; }
    public DbSet<PaymentGatewayEntity> PaymentGateways { get; set; }
    public DbSet<ProductMenuEntity> ProductMenus { get; set; }
    public DbSet<MenuEntity> Menus { get; set; }
    public DbSet<BusinessEntity> Business { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaymentEntity>()
            .HasOne(p => p.PaymentGateway)
            .WithMany(pg => pg.Payments)
            .HasForeignKey(p => p.PaymentGatewayID);

        modelBuilder.Entity<BusinessEntity>()
        .HasKey(b => b.ID);

        modelBuilder.Entity<MenuEntity>()
            .HasKey(m => m.ID);

        modelBuilder.Entity<MenuEntity>()
            .HasOne(m => m.Business)
            .WithMany(b => b.Menus)
            .HasForeignKey(m => m.BusinessID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserEntity>()
        .HasOne(u => u.Business)
        .WithOne(b => b.User)
        .HasForeignKey<BusinessEntity>(b => b.Email)
        .HasPrincipalKey<UserEntity>(u => u.Email);

        base.OnModelCreating(modelBuilder);
       // modelBuilder.ApplyConfiguration(new ChinhSachNhanSuConfiguration());
       
        ConfigureModel(modelBuilder);
    }
    private void ConfigureModel(ModelBuilder modelBuilder)
    {
        var user = new UserEntity
        {
            Email = "hhson365@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("123456"),
            Role = "Manager"
        };

        var user1 = new UserEntity
        {
            Email = "sonhhse172307@fpt.edu.vn",
            Password = BCrypt.Net.BCrypt.HashPassword("123456"),
            Role = "Business"
        };

        var manager = new ManagerEntity
        {
            Name = "Robin Son",
            Phone = "1234567890",
            Email = "hhson365@gmail.com" // Same email as user
        };

        var business = new BusinessEntity
        {
            Name = "Son Of The ...",
            Url = "https://firebasestorage.googleapis.com/v0/b/oderingkiosksystem.appspot.com/o/uploads%2F5dc9a5e5-ee95-4b31-9ca5-cb8b4cb1c120.jpg?alt=media&token=9b6f02db-62fd-488a-9234-efbcf3070265",
            BankAccountName = "Ho Huu Son",
            BankAccountNumber = 123456,
            BankName = "Techcombank",
            Email = "sonhhse172307@fpt.edu.vn" // Same email as user
        };

        var categories = new List<CategoryEntity>
        {
            new CategoryEntity { ID = 1, Name = "Soft Drinks", Url = "https://firebasestorage.googleapis.com/v0/b/oderingkiosksystem.appspot.com/o/uploads%2F40b64192-e4de-438f-9f6b-23b37888bf01.jpg?alt=media&token=5869429c-83c1-4fd8-b6be-e962dea9e1cc" },
            new CategoryEntity { ID = 2, Name = "Tea & Coffee", Url = "https://firebasestorage.googleapis.com/v0/b/oderingkiosksystem.appspot.com/o/uploads%2F40b64192-e4de-438f-9f6b-23b37888bf01.jpg?alt=media&token=5869429c-83c1-4fd8-b6be-e962dea9e1cc" },
            new CategoryEntity { ID = 3, Name = "Sports & Energy Drinks", Url = "https://firebasestorage.googleapis.com/v0/b/oderingkiosksystem.appspot.com/o/uploads%2F40b64192-e4de-438f-9f6b-23b37888bf01.jpg?alt=media&token=5869429c-83c1-4fd8-b6be-e962dea9e1cc" },
        };

        modelBuilder.Entity<UserEntity>().HasData(user);
        modelBuilder.Entity<UserEntity>().HasData(user1);
        modelBuilder.Entity<ManagerEntity>().HasData(manager);
        modelBuilder.Entity<BusinessEntity>().HasData(business);
        modelBuilder.Entity<CategoryEntity>().HasData(categories.ToArray());
    }
}
