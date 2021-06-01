using FluentValidation;
using Produto.Application.Command.Commands;
using System;

namespace Produto.Application.Command.Validators
{
    public class CriarProdutoCommandValidator : AbstractValidator<CriarProdutoCommand>
    {
        public CriarProdutoCommandValidator()
        {
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
