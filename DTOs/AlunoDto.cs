using FluentValidation;

namespace ApiDockerPiaget.DTOs
{

    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Serie { get; set; } = string.Empty;
        public int EscolaId { get; set; }
    }


}
