using DesafioBackendVertrau.Models.Enums;

namespace DesafioBackendVertrau.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public Genero Genero { get; set; }
    }
}
