
public class UsuarioDTO
{
    public int UsuarioId { get; set; }

    public string? Nome { get; set; }

    public string? Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    public int NivelAcesso { get; set; }

    public Endereco? Endereco { get; set; }

    public Telefone? Telefone { get; set; }
}