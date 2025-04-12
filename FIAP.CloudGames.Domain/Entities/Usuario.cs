using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Domain.Entities;

public class Usuario(string nome, Email email, Senha senha, ETipo tipo = ETipo.Usuario)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; } = nome;
    public Email Email { get; private set; } = email;
    internal Senha Senha { get; private set; } = senha;
    public ETipo Tipo { get; private set; } = tipo;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public void AtualizarNome(string novoNome) 
    {
        if (string.IsNullOrEmpty(novoNome))
            throw new ArgumentException("E necessario informar um novo nome.");
        
        Nome = novoNome;
    }

    public void AtualizarEmail(string novoEmail)
    {
        if (string.IsNullOrEmpty(novoEmail))
            throw new ArgumentException("E necessario informar um email.");
        
        Email = new Email(novoEmail);
    }

    public void AtualizarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrEmpty(novaSenhaHash))
            throw new ArgumentException("E necessario informar uma nova senha.");
        
        Senha = new Senha(novaSenhaHash);
    }

    public void PromoverParaAdmin()
    {
        if (Tipo == ETipo.Administrador)
            throw new InvalidOperationException("Usuário já é administrador.");

        Tipo = ETipo.Administrador;
    }
}