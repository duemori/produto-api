using Microsoft.EntityFrameworkCore;
using Produto.Domain.Context;
using Produto.Domain.Entities;
using Produto.Domain.Repositories;
using System.Threading.Tasks;

namespace Produto.Infrastructure.Command.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IProdutoContext _produtoContext;

        public ProdutoRepository(IProdutoContext produtoContext)
        {
            _produtoContext = produtoContext;
        }

        public async Task<ProdutoEntity> ObterPorIdAsync(int id)
        {
            return await _produtoContext.Produtos.AsTracking().SingleAsync(p => p.Id == id);
        }

        public async Task<int> InserirAsync(ProdutoEntity produto)
        {
            await _produtoContext.Produtos.AddAsync(produto);

            await _produtoContext.SaveChangesAsync();

            return produto.Id;
        }

        public async Task<bool> AtualizarAsync(ProdutoEntity produto)
        {
            _produtoContext.Produtos.Update(produto);

            return await _produtoContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var produto = await ObterPorIdAsync(id);

            _produtoContext.Produtos.Remove(produto);

            return await _produtoContext.SaveChangesAsync() > 0;
        }
    }
}
