
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }

    public string? Nome { get; set; }

    public string? Cpf { get; set; }

    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
    public DateTime DataNascimento { get; set; }
    public int NivelAcesso { get; set; }
    public virtual Email? Email {get;set;}
    public virtual Endereco? Endereco { get; set; }    
    public virtual Telefone? Telefone { get; set; }
}