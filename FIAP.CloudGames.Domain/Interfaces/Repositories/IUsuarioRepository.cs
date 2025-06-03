using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Shared;

namespace FIAP.CloudGames.Domain.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);
    Task<Usuario?> FindByEmailAsync(string email);
    Task<Usuario?> FindByIdAsync(Guid id);
    Task<bool> AlreadyExistsByEmailAsync(string email);
    Task DeleteAsync(Usuario usuario);
    Task SaveChangesAsync();
    public Task UpdateAsync(Usuario usuario);
}