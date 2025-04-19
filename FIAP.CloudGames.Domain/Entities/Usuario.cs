using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public Senha Senha { get; private set; }
    public ETipo Tipo { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Usuario(string nome, Email email, Senha senha, ETipo tipo = ETipo.Usuario)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException(nameof(nome));
        
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Senha = senha ?? throw new ArgumentNullException(nameof(senha));; 
        Tipo = tipo;
        Ativo = true;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void AtualizarNome(string novoNome) 
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("E necessario informar um novo nome.");
        
        Nome = novoNome;
    }

    public void AtualizarEmail(string novoEmail)
    {
        if (string.IsNullOrWhiteSpace(novoEmail))
            throw new ArgumentException("E necessario informar um email.");
        
        Email = new Email(novoEmail);
    }

    public void AtualizarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novaSenhaHash))
            throw new ArgumentException("E necessario informar uma nova senha.");

        var possivelNovaSenha = new Senha(novaSenhaHash);

        if (possivelNovaSenha.Equals(Senha))
            throw new InvalidOperationException("A nova senha não pode ser igual à anterior.");
        
        Senha = new Senha(novaSenhaHash);
    }

    public void PromoverParaAdmin()
    {
        if (Tipo == ETipo.Administrador)
            throw new InvalidOperationException("Usuário já é administrador.");

        Tipo = ETipo.Administrador;
    }

    public void Inativar() => Ativo = false;
    public void Ativar() => Ativo = true;
    
    public override bool Equals(object? obj)
    {
        if (obj is not Usuario other)
            return false;

        return Id == other.Id;
    }
}