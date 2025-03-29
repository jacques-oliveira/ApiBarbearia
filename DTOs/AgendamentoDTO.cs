using System.ComponentModel.DataAnnotations;

public class AgendamentoDTO{        
    
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}")]
    public DateTime DataAgendamento {get;set;}
    public string? NomeUsuario { get; set; }
    public string? NomeProduto {get; set;}
    public string? DescricaoProduto {get; set;}
    public decimal PrecoProduto {get; set;}
        
}