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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.Name).IsUnique();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
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

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            entity.HasOne<Product>()
                .WithMany(p => p.ProductCategories)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Category>()
                .WithMany()
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
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