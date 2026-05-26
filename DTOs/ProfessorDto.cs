namespace ApiDockerPiaget.DTOs
{
    public class EscolaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public List<AlunoDto> Alunos { get; set; } = new();
        public List<ProfessorDto> Professores { get; set; } = new();
    }

    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Serie { get; set; } = string.Empty;
        public int EscolaId { get; set; }
    }

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
