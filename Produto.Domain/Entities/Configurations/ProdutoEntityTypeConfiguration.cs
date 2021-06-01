using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Produto.Domain.Entities.Configurations
{
    public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<ProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Nome).HasColumnName("Nome");
            builder.Property(p => p.ValorVenda).HasColumnName("ValorVenda");
            builder.Property(p => p.Imagem).HasColumnName("Imagem");
        }
    }
}
