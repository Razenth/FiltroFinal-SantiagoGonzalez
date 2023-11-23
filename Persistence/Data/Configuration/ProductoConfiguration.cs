using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuracion;
public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("producto");

        builder.HasIndex(e => e.Gama, "gama");

        builder.Property(e => e.Id)
            .HasMaxLength(15)
            .HasColumnName("id");
            
        builder.Property(e => e.CantidadEnStock).HasColumnName("cantidad_en_stock");
        builder.Property(e => e.Descripcion)
            .HasColumnType("text")
            .HasColumnName("descripcion");
        builder.Property(e => e.Dimensiones)
            .HasMaxLength(25)
            .HasColumnName("dimensiones");
        builder.Property(e => e.Gama)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("gama");
        builder.Property(e => e.Nombre)
            .IsRequired()
            .HasMaxLength(70)
            .HasColumnName("nombre");
        builder.Property(e => e.PrecioProveedor)
            .HasPrecision(15, 2)
            .HasColumnName("precio_proveedor");
        builder.Property(e => e.PrecioVenta)
            .HasPrecision(15, 2)
            .HasColumnName("precio_venta");
        builder.Property(e => e.Proveedor)
            .HasMaxLength(50)
            .HasColumnName("proveedor");

        builder.HasOne(d => d.GamaNavigation).WithMany(p => p.Productos)
            .HasForeignKey(d => d.Gama)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("producto_ibfk_1");
    }
}