using FluentValidation;
using Produto.Application.Query.DAOs;
using Produto.Application.Query.Queries;

namespace Produto.Application.Query.Validators
{
    public class ProdutoQueryValidator : AbstractValidator<ProdutoQuery>
    {
        public ProdutoQueryValidator(IProdutoDao produtoDao)
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("Id inválido.")
                .DependentRules(() =>
                {
                    RuleFor(p => p.Id)
                        .MustAsync(async (id, cancellation) => await produtoDao.VerificarSeCadastradoAsync(id))
                        .WithMessage("Produto não cadastrado.");
                });
        }
    }
}
