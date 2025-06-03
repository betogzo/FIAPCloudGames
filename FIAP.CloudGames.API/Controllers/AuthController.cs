using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FIAP.CloudGames.Application.DTOs.Request.Auth;
using FIAP.CloudGames.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.CloudGames.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ILoginService loginService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto)
    {
        var result = await loginService.AutenticarAsync(dto.Email, dto.Senha);

        if (!result.IsSuccess)
            return Unauthorized(new { result.Errors });

        return Ok(new { token = result.Data });
    }
}