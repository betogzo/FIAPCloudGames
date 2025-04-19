namespace FIAP.CloudGames.Domain.Interfaces.Policies;

public interface ISenhaPolicy
{
    bool IsValid(string senha);
}