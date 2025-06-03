using System.ComponentModel.DataAnnotations;

namespace FIAP.CloudGames.Application.DTOs.Request.Usuario;

public class AtualizarUsuarioDto
{
    [EmailAddress(ErrorMessage = "Email Invalido!")]
    public string? Email { get; set; } = null!;
    
    public string? Nome { get; set; } = null!;
}
