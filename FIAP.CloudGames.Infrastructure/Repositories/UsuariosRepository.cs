using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Domain.ValueObjects;
using FIAP.CloudGames.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.CloudGames.Infrastructure.Repositories;

public class UsuariosRepository(AppDbContext dbContext) : IUsuarioRepository
{
    public async Task AddAsync(Usuario usuario)
    {
        dbContext.Usuarios.Add(usuario);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Usuario?> FindByEmailAsync(string email)
    {
        var emailVo = Email.Create(email);
        if (!emailVo.IsSuccess) return null;

        return await dbContext.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(emailVo.Data));
    }

    public async Task<Usuario?> FindByIdAsync(Guid id) => await dbContext.Usuarios.FindAsync(id);

    public async Task<bool> AlreadyExistsByEmailAsync(string email)
    {
        var emailVo = Email.Create(email);
        if (!emailVo.IsSuccess) return false;

        return await dbContext.Usuarios
            .AnyAsync(u => u.Email == emailVo.Data);
    }
    
    public async Task DeleteAsync(Usuario usuario)
    {
        dbContext.Usuarios.Remove(usuario);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Usuario usuario)
    {
        dbContext.Usuarios.Update(usuario);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}