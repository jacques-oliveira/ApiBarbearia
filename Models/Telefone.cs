using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Telefones")]
public class Telefone{
    [Key]
    public int TelefoneId { get; set; }
    [Required]
    [StringLength(11)]
    public string? Celular {get;set;}
    [StringLength(11)]
    public string? Fixo {get;set;}
    public Usuario? Usuario {get; set;}
}