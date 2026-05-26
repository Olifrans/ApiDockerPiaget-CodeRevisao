using FluentValidation;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Validators;

public class AlunoValidator : AbstractValidator<AlunoDto>
{
    public AlunoValidator()
    {
        RuleFor(a => a.Nome)
            .NotEmpty().WithMessage("Nome do aluno é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres");

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(a => a.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória")
            .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser no passado");

        RuleFor(a => a.EscolaId)
            .GreaterThan(0).WithMessage("EscolaId é obrigatório");
    }
}