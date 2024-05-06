using System.ComponentModel.DataAnnotations;

public class RegisterModelDTO {

    [Required(ErrorMessage ="Nome de usuário obriagtório")]
    public string? UserName { get; set; }

    [Required(ErrorMessage ="Password obrigatório")]
    public string? Password {get; set; }

    [EmailAddress]
    [Required(ErrorMessage ="Email é obrigatório")]
    public string? Email {get; set; }
}