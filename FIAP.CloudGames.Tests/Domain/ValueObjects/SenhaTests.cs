using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.ValueObjects;

public class SenhaTests
{
    [Fact]
    public void CriarSenha_ComHashValido_DeveArmazenarHash()
    {
        const string hashValido = "HASH::MinhaSenha123";
        var senhaValida = Senha.Create(hashValido);
        
        Assert.True(senhaValida.IsSuccess);
        Assert.Equal(hashValido, senhaValida.Data.ValorHash);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarSenha_ComHashNuloOuVazio_DeveResultarFalha(string hashInvalido)
    {
        var senha = Senha.Create(hashInvalido);
        
        Assert.False(senha.IsSuccess);
        Assert.Contains("vazio", senha.Errors[0], StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public void Senhas_ComMesmoHash_EqualsDeveSerTrue()
    {
        const string hash = "HASH::MinhaSenha123";
        var senha1 = Senha.Create(hash);
        Assert.True(senha1.IsSuccess);
        var senha2 = Senha.Create(hash);
        Assert.True(senha2.IsSuccess);
        
        Assert.True(senha1.Data.Equals(senha2.Data));
    }
    
    [Fact]
    public void Senhas_ComHashDistintos_EqualsDeveSerFalse()
    {
        var senha1 = Senha.Create("HASH::MinhaSenha123");
        Assert.True(senha1.IsSuccess);
        var senha2 = Senha.Create("HASH::MinhaSenhaDiferente321");
        Assert.True(senha2.IsSuccess);
        
        Assert.False(senha1.Data.Equals(senha2.Data));
    }
    
    [Fact]
    public void Senhas_ComMesmoHash_GetHashCode_EqualsDeveSerTrue()
    {
        const string hash = "HASH::MinhaSenha123";
        var senha1 = Senha.Create(hash);
        var senha2 = Senha.Create(hash);
        
        Assert.Equal(senha1.Data.GetHashCode(), senha2.Data.GetHashCode());
    }
    
    [Fact]
    public void Senha_ToString_DeveRetornarHash()
    {
        const string hash = "HASH::MinhaSenha123";
        var senha = Senha.Create(hash);
        
        Assert.True(senha.IsSuccess);
        Assert.Equal(hash, senha.Data.ToString());
    }
}