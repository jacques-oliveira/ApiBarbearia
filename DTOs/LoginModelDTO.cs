using System.ComponentModel.DataAnnotations;

public class LoginModelDTO {
    [Required(ErrorMessage ="Nome de usuário obriagtório")]
    public string? UserName { get; set; }

    [Required(ErrorMessage ="Password obrigatório")]
    public string? Password {get; set; }

}