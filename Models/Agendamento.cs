using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("Agendamentos")]
public class Agendamento{
    [Key]
    public int AgendamentoId { get; set; }
    [Required]
    public DateTime Data {get;set;}
    public int UsuarioId {get; set;}
    public int ProdutoId { get; set; }    
    public Usuario? Usuarios {get; set;}
    public Produto? Produtos {get;set;}
}