using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Produtos")]
public class Produto{
    [Key]
    public int ProdutoId { get; set; }
    [Required]
    [StringLength(80)]
    public string? Nome {get; set;}
    [Required]
    [StringLength(80)]
    public string? Descricao {get; set;}
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco {get; set;}
    [Required]
    [StringLength(300)]
    public string? ImagemUrl {get; set;}
    public int CategoriaId {get; set;}
    public ICollection<Agendamento>? Agendamentos {get;set;}                   
}