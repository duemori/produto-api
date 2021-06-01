using Microsoft.EntityFrameworkCore;
using Produto.Domain.Entities;
using Produto.Domain.Entities.Configurations;
using System.Threading.Tasks;

namespace Produto.Domain.Context
{
    public class ProdutoContext : DbContext, IProdutoContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) { }

        public DbSet<ProdutoEntity> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
