using MediatR;
using Produto.Application.Command.Commands;
using Produto.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Produto.Application.Command.Handlers
{
    public class RemoverProdutoCommandHandler : IRequestHandler<RemoverProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;

        public RemoverProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> Handle(RemoverProdutoCommand request, CancellationToken cancellationToken)
        {
            return await _produtoRepository.RemoverAsync(request.Id);
        }
    }
}
