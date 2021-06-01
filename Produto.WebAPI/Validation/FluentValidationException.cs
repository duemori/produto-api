using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Produto.WebAPI.Validation
{
    public class FluentValidationException : Exception
    {
        public int Status { get; private set; } = StatusCodes.Status400BadRequest;
        public string Mensagem { get; private set; }

        public FluentValidationException() { }

        public FluentValidationException(List<ValidationFailure> failures)
        {
            Mensagem = string.Join("\r\n", failures);
        }
    }
}
