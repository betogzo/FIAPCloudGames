namespace FIAP.CloudGames.Domain.Interfaces.Policies;

public interface IValidateSenhaService
{
    bool IsValid(string senha);
}