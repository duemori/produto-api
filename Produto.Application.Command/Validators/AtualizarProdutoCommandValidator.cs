using FluentValidation;
using Produto.Application.Command.Commands;
using Produto.Application.Query.DAOs;
using System;

namespace Produto.Application.Command.Validators
{
    public class AtualizarProdutoCommandValidator : AbstractValidator<AtualizarProdutoCommand>
    {
        public AtualizarProdutoCommandValidator(IProdutoDao produtoDao)
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

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage("Obrigatório informar o nome.")
                .MaximumLength(100)
                    .WithMessage("O nome pode conter no máximo 100 caracteres.");

            RuleFor(p => p.ValorVenda)
                .GreaterThan(0)
                    .WithMessage("Obrigatório informar o valor de venda.");

            RuleFor(p => p.Imagem)
                .Must(i =>
                {
                    Span<byte> buffer = new Span<byte>(new byte[i.Length]);
                    return Convert.TryFromBase64String(i, buffer, out int _);
                })
                    .WithMessage("Imagem inválida.")
                .When(p => !string.IsNullOrWhiteSpace(p.Imagem));
        }
    }
}
