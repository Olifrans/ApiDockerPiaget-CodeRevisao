using FluentValidation;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Validators;

public class EscolaValidator : AbstractValidator<EscolaDto>
{
    public EscolaValidator()
    {
        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("Nome da escola é obrigatório")
            .MinimumLength(5);

        RuleFor(e => e.Cidade)
            .NotEmpty().WithMessage("Cidade é obrigatória");
    }
}