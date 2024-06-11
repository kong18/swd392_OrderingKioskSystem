﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingKioskSystem.Infrastructure.Persistence;

#nullable disable

namespace SWD.OrderingKioskSystem.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.BusinessEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BankAccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankAccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BinId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Business");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.KioskEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Kiosk");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ManagerEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Manager/Staff");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.MenuEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BusinessID");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.OrderDetailEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.OrderEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("KioskID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipperID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ID");

                    b.HasIndex("KioskID");

                    b.HasIndex("ShipperID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.PaymentEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentGatewayID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("PaymentGatewayID");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.PaymentGatewayEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PaymentGateway");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ProductEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BusinessID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ProductMenuEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MenuID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("MenuID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductMenu");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ShipperEntity", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhatCuoi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXoa")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhatID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiTaoID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NguoiXoaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Shipper");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.UserEntity", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.BusinessEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.UserEntity", "User")
                        .WithOne("Business")
                        .HasForeignKey("OrderingKioskSystem.Domain.Entities.BusinessEntity", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ManagerEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.UserEntity", "User")
                        .WithOne("Manager")
                        .HasForeignKey("OrderingKioskSystem.Domain.Entities.ManagerEntity", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.MenuEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.BusinessEntity", "Business")
                        .WithMany("Menus")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.OrderDetailEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.OrderEntity", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingKioskSystem.Domain.Entities.ProductEntity", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.OrderEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.KioskEntity", "Kiosk")
                        .WithMany("Order")
                        .HasForeignKey("KioskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingKioskSystem.Domain.Entities.ShipperEntity", "Shipper")
                        .WithMany("Orders")
                        .HasForeignKey("ShipperID");

                    b.Navigation("Kiosk");

                    b.Navigation("Shipper");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.PaymentEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.OrderEntity", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingKioskSystem.Domain.Entities.PaymentGatewayEntity", "PaymentGateway")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentGatewayID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("PaymentGateway");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ProductEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.BusinessEntity", "Business")
                        .WithMany("Products")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingKioskSystem.Domain.Entities.CategoryEntity", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ProductMenuEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.MenuEntity", "Menu")
                        .WithMany("ProductMenus")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingKioskSystem.Domain.Entities.ProductEntity", "Product")
                        .WithMany("ProductMenus")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ShipperEntity", b =>
                {
                    b.HasOne("OrderingKioskSystem.Domain.Entities.UserEntity", "User")
                        .WithOne("Shipper")
                        .HasForeignKey("OrderingKioskSystem.Domain.Entities.ShipperEntity", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.BusinessEntity", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.KioskEntity", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.MenuEntity", b =>
                {
                    b.Navigation("ProductMenus");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.OrderEntity", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.PaymentGatewayEntity", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ProductEntity", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductMenus");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.ShipperEntity", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OrderingKioskSystem.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("Business")
                        .IsRequired();

                    b.Navigation("Manager")
                        .IsRequired();

                    b.Navigation("Shipper")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
