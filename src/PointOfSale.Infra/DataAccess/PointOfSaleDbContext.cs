using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Infra.DataAccess;

public class PointOfSaleDbContext : DbContext
{
    public PointOfSaleDbContext(DbContextOptions<PointOfSaleDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<OrderItem> OrdersItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Tables Configs

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(t => t.Number)
            .IsRequired();

            entity.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.HasIndex(c => c.Name).IsUnique();

            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

            entity.Navigation(a => a.Products)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.SKU).IsUnique();

            entity.Navigation(p => p.ProductCategories)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            entity.Navigation(p => p.ProductAttachments)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Path)
                .IsRequired()
                .HasMaxLength(2083);

            entity.Property(a => a.Extension)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(a => a.Size)
                .IsRequired();

            entity.Navigation(a => a.Products)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        });

        modelBuilder.Entity<Order>(entity =>
        {

            entity.HasKey(p => p.Id);

            entity.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            //entity.HasOne<Customer>()
            //      .WithMany()
            //      .HasForeignKey(o => o.CustomerId);

            entity.Navigation(o => o.Items)
              .UsePropertyAccessMode(PropertyAccessMode.Field);

        });

        modelBuilder.Entity<OrderItem>(entity =>
        {

            entity.HasKey(oi => new { oi.OrderId, oi.ProductId });

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            entity.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

        });

        // Table Relationships 
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(pc => new
            {

                pc.ProductId,
                pc.CategoryId
            });

            entity.HasOne(x => x.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductAttachment>(entity =>
        {
            entity.HasKey(x => new
            {
                x.ProductId,
                x.AttachmentId
            });

            entity.HasOne(x => x.Product)
                .WithMany(x => x.ProductAttachments)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Attachment)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.AttachmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}