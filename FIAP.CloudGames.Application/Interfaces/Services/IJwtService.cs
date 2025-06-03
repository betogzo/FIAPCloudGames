using FIAP.CloudGames.Domain.Entities;

namespace FIAP.CloudGames.Application.Interfaces.Services;

public interface IJwtService
{
    string GerarToken(Usuario usuario);
}
