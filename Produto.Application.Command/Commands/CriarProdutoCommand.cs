using MediatR;

namespace Produto.Application.Command.Commands
{
    public class CriarProdutoCommand : IRequest<int>
    {
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Imagem { get; set; }
    }
}
