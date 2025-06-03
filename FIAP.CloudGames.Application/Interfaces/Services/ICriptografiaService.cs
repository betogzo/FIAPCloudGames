namespace FIAP.CloudGames.Application.Interfaces.Services;

public interface ICriptografiaService
{
    string HashSenha(string senha);
    bool ValidaSenha(string senha, string hashSenha);
}