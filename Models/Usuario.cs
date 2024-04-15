using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("Usuarios")]
public class Usuario{
    [Key]
    public int UsuarioId { get; set; }
    [Required]
    [StringLength(80)]
    public string? Nome {get;set;}
    [Required]
    [StringLength(11)]
    public string? Cpf {get; set;}
    [Required]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
    public DateTime DataNascimento { get; set; }
    [Column(TypeName =("Int(1)"))]
    public int NivelAcesso {get; set;}    
    [JsonIgnore]
    public ICollection<Agendamento>? Agendamentos {get;set;}   
    [JsonIgnore]
    public virtual Email? Email {get;set;}       
    [JsonIgnore]
    public virtual Endereco? Endereco {get; set;}         
    [JsonIgnore]
    public virtual Telefone? Telefone {get;set;}
}