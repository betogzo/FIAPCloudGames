using BCrypt.Net;
using FIAP.CloudGames.Application.Interfaces.Services;

namespace FIAP.CloudGames.Infrastructure.Services;

public class HashService : ICriptografiaService
{
    public string HashSenha(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool ValidaSenha(string senha, string hashSenha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hashSenha);
    }
}
