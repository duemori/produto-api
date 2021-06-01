using Bogus;
using Bogus.Extensions;
using Produto.Application.Command.Commands;

namespace Produto.Tests
{
    class ProdutoFaker
    {
        public static Faker<AtualizarProdutoCommand> AtualizarProdutoCommand { get; } =
            new Faker<AtualizarProdutoCommand>()
                .RuleFor(p => p.Id, f => f.Random.Int(1))
                .RuleFor(p => p.Nome, f => f.Commerce.ProductDescription().ClampLength(1, 100))
                .RuleFor(p => p.ValorVenda, f => f.Random.Decimal(0.01m, 999.99m))
                .RuleFor(p => p.Imagem, _ => "JMiyB19G2beD5Ns/ieOXdg==");
    }
}
