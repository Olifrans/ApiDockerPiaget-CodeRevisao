namespace ApiDockerPiaget.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Serie { get; set; } = string.Empty;

        public int EscolaId { get; set; }
        public Escola Escola { get; set; } = null!;
    }
}
