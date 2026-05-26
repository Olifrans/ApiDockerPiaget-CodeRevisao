using FluentValidation;

namespace ApiDockerPiaget.DTOs
{


    public class ProfessorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public string Titulacao { get; set; } = string.Empty;
        public int EscolaId { get; set; }
    }



}
