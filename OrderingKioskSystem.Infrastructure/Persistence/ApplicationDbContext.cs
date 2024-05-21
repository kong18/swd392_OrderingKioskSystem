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

        base.OnModelCreating(modelBuilder);
       // modelBuilder.ApplyConfiguration(new ChinhSachNhanSuConfiguration());
       
        ConfigureModel(modelBuilder);
    }
    private void ConfigureModel(ModelBuilder modelBuilder)
    {


    }
}
