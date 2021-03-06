﻿using FluentValidation;
using RCM.Domain.Commands.ProdutoCommands;

namespace RCM.Domain.Validators.ProdutoCommandValidators
{
    public class RemoveProdutoAplicacaoCommandValidator : ProdutoCommandValidator<RemoveProdutoAplicacaoCommand>
    {
        public RemoveProdutoAplicacaoCommandValidator()
        {
            ValidateId();
            ValidateAplicacaoId();
        }

        private void ValidateAplicacaoId()
        {
            RuleFor(ap => ap.AplicacaoId)
                .NotEmpty();
        }
    }
}
