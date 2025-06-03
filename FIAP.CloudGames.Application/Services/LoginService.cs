using FIAP.CloudGames.Application.Interfaces.Services;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Domain.Shared;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Application.Services;

public class LoginService(
    IUsuarioRepository usuarioRepository,
    ICriptografiaService criptografiaService,
    IJwtService jwtService
) : ILoginService
{
    public async Task<Result<string>> AutenticarAsync(string email, string senha)
    {
        var usuario = await usuarioRepository.FindByEmailAsync(email);
        if (usuario is null)
            return Result<string>.Fail(["Credenciais inválidas."]);

        if (!criptografiaService.ValidaSenha(senha, usuario.Senha.ValorHash))
            return Result<string>.Fail(["Credenciais inválidas."]);

        var token = jwtService.GerarToken(usuario);
        return Result<string>.Ok(token);
    }
}
