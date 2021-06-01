using MediatR;

namespace Produto.Application.Command.Commands
{
    public class AtualizarProdutoCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Imagem { get; set; }
    }
}
