using System.Text.Json.Serialization;

namespace ApiDockerPiaget.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public string Titulacao { get; set; } = string.Empty;

        public int EscolaId { get; set; }

        [JsonIgnore] // Ciclo de Referência (Circular Reference) entre Escola  Aluno  Escola. Isso acontece porque o Entity Framework carrega os relacionamentos bidirecionais e o System.Text.Json não sabe como serializar isso.
        public Escola Escola { get; set; } = null!;
    }
}
