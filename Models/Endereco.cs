using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Enderecos")]
public class Endereco{
    [Key]
    public int EnderecoId { get; set; }
    [Required]
    [StringLength(80)]
    public string? Logradouro {get;set;}
    public int Numero {get;set;}
    [Required]
    [StringLength(80)]
    public string? Bairro {get; set;}
    [Required]
    [StringLength(9)]
    public string? Cep {get;set;}
    public int UsuarioId {get;set;}
}