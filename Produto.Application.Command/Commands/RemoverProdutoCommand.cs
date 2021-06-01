using MediatR;

namespace Produto.Application.Command.Commands
{
    public class RemoverProdutoCommand : IRequest<bool>
    {
        public int Id { get; private set; }

        public RemoverProdutoCommand(int id)
        {
            Id = id;
        }
    }
}
