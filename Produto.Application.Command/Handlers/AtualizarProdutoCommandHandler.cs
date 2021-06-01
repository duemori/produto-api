using AutoMapper;
using MediatR;
using Produto.Application.Command.Commands;
using Produto.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Produto.Application.Command.Handlers
{
    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public AtualizarProdutoCommandHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(request.Id);

            _mapper.Map(request, produto);

            return await _produtoRepository.AtualizarAsync(produto);
        }
    }
}
