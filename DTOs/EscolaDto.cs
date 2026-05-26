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


}
