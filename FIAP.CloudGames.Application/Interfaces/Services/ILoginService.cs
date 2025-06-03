using FIAP.CloudGames.Domain.Shared;

namespace FIAP.CloudGames.Application.Interfaces.Services;

public interface ILoginService
{
    Task<Result<string>> AutenticarAsync(string email, string senha);
}
