using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuracion;
public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("pedido");

        builder.HasIndex(e => e.CodigoCliente, "codigo_cliente");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");
        builder.Property(e => e.Comentarios)
            .HasColumnType("text")
            .HasColumnName("comentarios");
        builder.Property(e => e.Estado)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("estado");
        builder.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
        builder.Property(e => e.FechaEsperada).HasColumnName("fecha_esperada");
        builder.Property(e => e.FechaPedido).HasColumnName("fecha_pedido");

        builder.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Pedidos)
            .HasForeignKey(d => d.CodigoCliente)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("pedido_ibfk_1");
    }
}