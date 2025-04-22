using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;
using FIAP.CloudGames.Domain.Shared;

namespace FIAP.CloudGames.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public Senha Senha { get; private set; }
    public ETipo Tipo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Usuario(string nome, Email email, Senha senha, ETipo tipo = ETipo.Usuario)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Senha = senha;
        Tipo = tipo;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static Result<Usuario> Create(string nome, string enderecoEmail, string senhaHash,
        ETipo tipo = ETipo.Usuario)
    {
        var listaErros = new List<string>();

        if (string.IsNullOrWhiteSpace(nome))
            listaErros.Add("Nome nao pode ser nulo ou vazio.");

        var resultadoEmail = Email.Create(enderecoEmail);
        var resultadoSenha = Senha.Create(senhaHash);

        if (!resultadoEmail.IsSuccess) listaErros.AddRange(resultadoEmail.Errors);
        if (!resultadoSenha.IsSuccess) listaErros.AddRange(resultadoSenha.Errors);

        return listaErros.Count != 0
            ? Result<Usuario>.Fail(listaErros)
            : Result<Usuario>.Ok(new Usuario(nome, resultadoEmail.Data, resultadoSenha.Data, tipo));
    }

    public Result AtualizarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            return Result.Fail(new List<string> { "Nome nao pode ser nulo ou vazio." });

        Nome = novoNome;
        UpdatedAt = DateTime.UtcNow;
        return Result.Ok();
    }

    public Result AtualizarEmail(string novoEmail)
    {
        var resultadoEmail = Email.Create(novoEmail);

        if (!resultadoEmail.IsSuccess)
            return Result.Fail(resultadoEmail.Errors);

        Email = resultadoEmail.Data;
        UpdatedAt = DateTime.UtcNow;
        return Result.Ok();
    }

    public Result AtualizarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novaSenhaHash))
            return Result.Fail(["E necessario informar uma nova senha."]);

        var resultadoSenha = Senha.Create(novaSenhaHash);
        
        if (!resultadoSenha.IsSuccess)
            return Result.Fail(resultadoSenha.Errors);

        if (resultadoSenha.Data.Equals(Senha))
            return Result.Fail(["A nova senha não pode ser igual à anterior."]);
        
        Senha = resultadoSenha.Data;
        UpdatedAt = DateTime.UtcNow;
        return Result.Ok();
    }

    public Result PromoverParaAdmin()
    {
        if (Tipo == ETipo.Administrador)
            return Result.Fail(new List<string> { "Usuário já é administrador." });

        Tipo = ETipo.Administrador;
        UpdatedAt = DateTime.UtcNow;
        return Result.Ok();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Usuario other)
            return false;

        return Id == other.Id;
    }
}