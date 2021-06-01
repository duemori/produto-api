using MediatR;
using Produto.Application.Query.DAOs;
using Produto.Application.Query.Queries;
using Produto.Application.Query.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Produto.Application.Query.Handlers
{
    public class ProdutoQueryHandler : IRequestHandler<ProdutoQuery, ProdutoViewModel>
    {
        private readonly IProdutoDao _produtoDao;

        public ProdutoQueryHandler(IProdutoDao produtoDao)
        {
            _produtoDao = produtoDao;
        }

        public async Task<ProdutoViewModel> Handle(ProdutoQuery request, CancellationToken cancellationToken)
        {
            return await _produtoDao.ObterPorIdAsync(request.Id);
        }
    }
}
