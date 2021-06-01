using FluentValidation;
using Produto.Application.Command.Commands;
using Produto.Application.Query.DAOs;

namespace Produto.Application.Command.Validators
{
    public class RemoverProdutoCommandValidator : AbstractValidator<RemoverProdutoCommand>
    {
        public RemoverProdutoCommandValidator(IProdutoDao produtoDao)
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
