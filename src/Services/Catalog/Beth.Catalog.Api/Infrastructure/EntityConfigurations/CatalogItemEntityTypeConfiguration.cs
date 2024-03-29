using Beth.Catalog.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beth.Catalog.Api.Infrastructure.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");

        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_item_hilo")
            .IsRequired();
        
        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(ci => ci.Price)
            .IsRequired();
        
        builder.Property(ci => ci.PictureFileName)
            .IsRequired(false);
        
        builder.Ignore(ci => ci.PictureUri);
        
        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}