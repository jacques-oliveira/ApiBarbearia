using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Emails")]
public class Email{
    [Key]
    public int EmailId { get; set; }
    [Required]
    [StringLength(80)]
    public string? EnderecoEmail {get; set;}
    public int UsuarioId {get; set;}
}