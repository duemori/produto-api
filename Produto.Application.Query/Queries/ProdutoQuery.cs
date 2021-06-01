using MediatR;
using Produto.Application.Query.ViewModels;

namespace Produto.Application.Query.Queries
{
    public class ProdutoQuery : IRequest<ProdutoViewModel>
    {
        public int Id { get; private set; }

        public ProdutoQuery(int id)
        {
            Id = id;
        }
    }
}
