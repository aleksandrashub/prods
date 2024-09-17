using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductsManufacturer.Models;

namespace ProductsManufacturer.Context;

public partial class User724Context : DbContext
{
    public User724Context()
    {
    }

    public User724Context(DbContextOptions<User724Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleTovar> SaleTovars { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tovar> Tovars { get; set; }

    public virtual DbSet<TovarDop> TovarDops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.2.159;Database=user724;Username=user724;password=68202");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManufacturer).HasName("manufacturer_pk");

            entity.ToTable("manufacturer", "tovar");

            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");
            entity.Property(e => e.NameManufacturer)
                .HasColumnType("character varying")
                .HasColumnName("name_manufacturer");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSale).HasName("sales_pk");

            entity.ToTable("sales", "tovar");

            entity.Property(e => e.IdSale).HasColumnName("id_sale");
            entity.Property(e => e.Date).HasColumnName("date");
        });

        modelBuilder.Entity<SaleTovar>(entity =>
        {
            entity.HasKey(e => new { e.IdSale, e.IdTovar }).HasName("sale_tovar_pk");

            entity.ToTable("sale_tovar", "tovar");

            entity.Property(e => e.IdSale).HasColumnName("id_sale");
            entity.Property(e => e.IdTovar).HasColumnName("id_tovar");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.SaleTovars)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_tovar_sales_fk");

            entity.HasOne(d => d.IdTovarNavigation).WithMany(p => p.SaleTovars)
                .HasForeignKey(d => d.IdTovar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_tovar_tovar_fk");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("newtable_pk");

            entity.ToTable("status", "tovar");

            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.NameStatus)
                .HasColumnType("character varying")
                .HasColumnName("name_status");
        });

        modelBuilder.Entity<Tovar>(entity =>
        {
            entity.HasKey(e => e.IdTovar).HasName("tovar_pk");

            entity.ToTable("tovar", "tovar");

            entity.Property(e => e.IdTovar).HasColumnName("id_tovar");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.IdMainDop).HasColumnName("id_main_dop");
            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.NameTovar)
                .HasColumnType("character varying")
                .HasColumnName("name_tovar");

            entity.HasOne(d => d.IdMainDopNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.IdMainDop)
                .HasConstraintName("tovar_tovar_dop_fk");

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.IdManufacturer)
                .HasConstraintName("tovar_manufacturer_fk");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("tovar_status_fk");
        });

        modelBuilder.Entity<TovarDop>(entity =>
        {
            entity.HasKey(e => e.IdMainDop).HasName("tovar_dop_pk");

            entity.ToTable("tovar_dop", "tovar");

            entity.Property(e => e.IdMainDop).HasColumnName("id_main_dop");
            entity.Property(e => e.IdDopTov).HasColumnName("id_dop_tov");
            entity.Property(e => e.IdMainTov).HasColumnName("id_main_tov");

            entity.HasOne(d => d.IdDopTovNavigation).WithMany(p => p.TovarDopIdDopTovNavigations)
                .HasForeignKey(d => d.IdDopTov)
                .HasConstraintName("tovar_dop_tovar_fk");

            entity.HasOne(d => d.IdMainTovNavigation).WithMany(p => p.TovarDopIdMainTovNavigations)
                .HasForeignKey(d => d.IdMainTov)
                .HasConstraintName("tovar_dop2_tovar_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
