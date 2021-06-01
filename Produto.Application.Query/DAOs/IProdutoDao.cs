using Produto.Application.Query.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produto.Application.Query.DAOs
{
    public interface IProdutoDao
    {
        Task<ProdutoViewModel> ObterPorIdAsync(int id);
        Task<IEnumerable<ProdutoViewModel>> ObterTodosAsync();
        Task<bool> VerificarSeCadastradoAsync(int id);
    }
}
