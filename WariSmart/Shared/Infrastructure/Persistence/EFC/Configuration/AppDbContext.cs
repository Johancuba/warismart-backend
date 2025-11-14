using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Model.Entities;
using WariSmart.API.IAM.Domain.Model.Aggregates;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Product Entity Configuration
        builder.Entity<Product>().HasKey(p => p.IdProducto);
        builder.Entity<Product>().Property(p => p.IdProducto).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.NombreProducto).IsRequired().HasMaxLength(200);
        builder.Entity<Product>().Property(p => p.SKU).IsRequired().HasMaxLength(50);
        builder.Entity<Product>().Property(p => p.Categoria).IsRequired().HasMaxLength(100);
        builder.Entity<Product>().Property(p => p.Ubicacion).IsRequired().HasMaxLength(100);
        builder.Entity<Product>().Property(p => p.Precio).IsRequired().HasColumnType("decimal(10,2)");
        builder.Entity<Product>().Property(p => p.Estado).IsRequired().HasMaxLength(50);
        builder.Entity<Product>().ToTable("productos");

        // Sale Entity Configuration
        builder.Entity<Sale>().HasKey(s => s.IdVenta);
        builder.Entity<Sale>().Property(s => s.IdVenta).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(s => s.Cliente).IsRequired().HasMaxLength(200);
        builder.Entity<Sale>().Property(s => s.DNIRUC).IsRequired().HasMaxLength(20);
        builder.Entity<Sale>().Property(s => s.MetodoPago).IsRequired().HasMaxLength(50);
        builder.Entity<Sale>().Property(s => s.TotalVenta).IsRequired().HasColumnType("decimal(10,2)");
        builder.Entity<Sale>().Property(s => s.FechaVenta).IsRequired();
        builder.Entity<Sale>().HasMany(s => s.Items).WithOne().HasForeignKey(i => i.IdVenta);
        builder.Entity<Sale>().ToTable("ventas");

        // SaleItem Entity Configuration
        builder.Entity<SaleItem>().HasKey(i => i.IdItem);
        builder.Entity<SaleItem>().Property(i => i.IdItem).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SaleItem>().Property(i => i.NombreProducto).IsRequired().HasMaxLength(200);
        builder.Entity<SaleItem>().Property(i => i.PrecioUnitario).IsRequired().HasColumnType("decimal(10,2)");
        builder.Entity<SaleItem>().Property(i => i.Subtotal).IsRequired().HasColumnType("decimal(10,2)");
        builder.Entity<SaleItem>().ToTable("venta_items");

        // User Entity Configuration
        builder.Entity<User>().HasKey(u => u.IdUser);
        builder.Entity<User>().Property(u => u.IdUser).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(255);
        builder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(50);
        builder.Entity<User>().ToTable("users");

        builder.UseSnakeCaseNamingConvention();
    }
}