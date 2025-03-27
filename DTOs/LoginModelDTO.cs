using System.ComponentModel.DataAnnotations;

public class LoginModelDTO {
    [Required(ErrorMessage ="Nome de usuário obriagtório")]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Password obrigatório")]
    public string? Password {get; set; }

}