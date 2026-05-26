
using AutoMapper;
using ApiDockerPiaget.Models;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos bidirecionais
        CreateMap<Escola, EscolaDto>().ReverseMap();
        CreateMap<Aluno, AlunoDto>().ReverseMap();
        CreateMap<Professor, ProfessorDto>().ReverseMap();

        // Configurações personalizadas (se necessário)
        CreateMap<Escola, EscolaDto>()
            .ForMember(dest => dest.Alunos, opt => opt.MapFrom(src => src.Alunos))
            .ForMember(dest => dest.Professores, opt => opt.MapFrom(src => src.Professores));
    }
}