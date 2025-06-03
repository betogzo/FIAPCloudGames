using FIAP.CloudGames.Application.DTOs.Request.Usuario;
using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.Shared;

namespace FIAP.CloudGames.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<Result<Guid>> RegistrarAsync(RegistrarUsuarioDto dto);
    Task<Result> AtualizarAsync(Guid id, AtualizarUsuarioDto dto);
    Task<Result> AlterarSenhaAsync(Guid id, string senhaAtual, string senhaNova);
    Task<Result> RemoverAsync(Guid id);
    Task<Result> AlterarTipo(Guid id, ETipo tipo);
}