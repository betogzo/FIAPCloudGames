using System.ComponentModel.DataAnnotations;

namespace FIAP.CloudGames.Application.DTOs.Request.Auth;

public class LoginUsuarioDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Senha { get; init; }
}
