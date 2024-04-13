
using System.Text.Json.Serialization;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }

    public string? Nome { get; set; }

    public string? Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    public int NivelAcesso { get; set; }

    [JsonIgnore]
    public Endereco? Endereco { get; set; }

    [JsonIgnore]
    public Telefone? Telefone { get; set; }
}