using Produto.Domain.Entities;
using System.Threading.Tasks;

namespace Produto.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<ProdutoEntity> ObterPorIdAsync(int id);
        Task<int> InserirAsync(ProdutoEntity produto);
        Task<bool> AtualizarAsync(ProdutoEntity produto);
        Task<bool> RemoverAsync(int id);
    }
}
