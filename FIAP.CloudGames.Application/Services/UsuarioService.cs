using FIAP.CloudGames.Application.DTOs.Request.Usuario;
using FIAP.CloudGames.Application.Interfaces.Services;
using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.Interfaces.Policies;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Domain.Shared;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Application.Services;

public class UsuarioService(
    IUsuarioRepository usuarioRepository,
    IValidateSenhaService validateSenhaService,
    ICriptografiaService hashService)
    : IUsuarioService
{
    public async Task<Result<Guid>> RegistrarAsync(RegistrarUsuarioDto dto)
    {
        List<string> listaErros = new();
        try
        {
            if (await usuarioRepository.AlreadyExistsByEmailAsync(dto.Email))
                listaErros.Add("Endereco de e-mail ja cadastrado.");

            if (!validateSenhaService.IsValid(dto.Senha))
                listaErros.Add("A senha informada é muito fraca e não atende aos requisitos.");

            var hash = hashService.HashSenha(dto.Senha);

            var usuario = Usuario.Create(dto.Nome, dto.Email, hash);
            if (!usuario.IsSuccess)
                listaErros.AddRange(usuario.Errors);

            if (listaErros.Count > 0)
                return Result<Guid>.Fail(listaErros);

            await usuarioRepository.AddAsync(usuario.Data);
            return Result<Guid>.Ok(usuario.Data.Id);
        }
        catch (Exception e)
        {
            return Result<Guid>.Fail([e.Message]);
        }
    }

    public async Task<Result> AtualizarAsync(Guid id, AtualizarUsuarioDto dto)
    {
        List<string> listaErros = [];

        try
        {
            var usuario = await usuarioRepository.FindByIdAsync(id);

            if (usuario is null)
                return Result.Fail(["Usuario inexistente."]);

            if (!string.IsNullOrWhiteSpace(dto.Nome) && !dto.Nome.Equals(usuario.Nome))
            {
                var resultadoAtualizaNome = usuario.AtualizarNome(dto.Nome);
                if (!resultadoAtualizaNome.IsSuccess)
                    listaErros.AddRange(resultadoAtualizaNome.Errors);
            }

            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != usuario.Email.ToString())
            {
                var novoEmail = Email.Create(dto.Email);
                
                if (!novoEmail.IsSuccess)
                    listaErros.AddRange(novoEmail.Errors);
                else
                {
                    var resultadoAtualizarEmail = usuario.AtualizarEmail(dto.Email);
                    if (!resultadoAtualizarEmail.IsSuccess)
                        listaErros.AddRange(resultadoAtualizarEmail.Errors);
                }
            }
            
            if (listaErros.Count > 0)
                return Result.Fail(listaErros);
            
            await usuarioRepository.UpdateAsync(usuario);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail([e.Message]);
        }
    }

    public async Task<Result> AlterarSenhaAsync(Guid id, string senhaAtual, string senhaNova)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoverAsync(Guid id)
    {
        var usuario = await usuarioRepository.FindByIdAsync(id);
        if (usuario is null) return Result.Fail(["Usuario inexistente."]);
        
        await usuarioRepository.DeleteAsync(usuario);
        return Result.Ok();
    }

    public async Task<Result> AlterarTipo(Guid id, ETipo tipo)
    {
        throw new NotImplementedException();
    }
}