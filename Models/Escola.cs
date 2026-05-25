namespace ApiDockerPiaget.Models
{
    public class Escola
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
        public ICollection<Professor> Professores { get; set; } = new List<Professor>();
    }
}
