using System.Security.Claims;
using FIAP.CloudGames.Application.DTOs.Request.Usuario;
using FIAP.CloudGames.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.CloudGames.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(IUsuarioService usuarioService, ILoginService loginService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(RegistrarUsuarioDto dto)
    {
        var result = await usuarioService.RegistrarAsync(dto);
        
        if (!result.IsSuccess)
            return BadRequest(new { result.Errors });
        
        return Ok(
            new { id = result.Data }
        );

        return Ok();
    }

    [Authorize]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id,AtualizarUsuarioDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        
        var isOwner = userId is not null && string.Equals(userId, id.ToString(), StringComparison.CurrentCultureIgnoreCase);
        var isAdmin = userRole == "Administrador";

        if (!isOwner && !isAdmin)
            return Forbid();
        
        await usuarioService.AtualizarAsync(id, dto);
        
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await usuarioService.RemoverAsync(id);
        
        return NoContent();
    }
}