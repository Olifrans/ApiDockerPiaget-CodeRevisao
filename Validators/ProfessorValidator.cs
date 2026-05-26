using FluentValidation;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Validators;

public class ProfessorValidator : AbstractValidator<ProfessorDto>
{
    public ProfessorValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("Nome do professor é obrigatório")
            .MinimumLength(3);

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress();

        RuleFor(p => p.Disciplina)
            .NotEmpty().WithMessage("Disciplina é obrigatória");

        RuleFor(p => p.EscolaId)
            .GreaterThan(0).WithMessage("EscolaId é obrigatório");
    }
}