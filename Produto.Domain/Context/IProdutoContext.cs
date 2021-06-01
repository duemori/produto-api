using Microsoft.EntityFrameworkCore;
using Produto.Domain.Entities;
using System.Threading.Tasks;

namespace Produto.Domain.Context
{
    public interface IProdutoContext
    {
        DbSet<ProdutoEntity> Produtos { get; set; }

        Task<int> SaveChangesAsync();
    }
}
