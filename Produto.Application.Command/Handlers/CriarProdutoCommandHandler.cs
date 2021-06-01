using AutoMapper;
using MediatR;
using Produto.Application.Command.Commands;
using Produto.Domain.Entities;
using Produto.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Produto.Application.Command.Handlers
{
    public class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, int>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public CriarProdutoCommandHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _mapper.Map<ProdutoEntity>(request);
            return await _produtoRepository.InserirAsync(produto);
        }
    }
}
