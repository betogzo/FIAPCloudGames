using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.ValueObjects;

public class SenhaTests
{
    [Fact]
    public void CriarSenha_ComHashValido_DeveArmazenarHash()
    {
        const string hashValido = "HASH::MinhaSenha123";
        var senhaValida = new Senha(hashValido);

        Assert.Equal(hashValido, senhaValida.ValorHash);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarSenha_ComHashNuloOuVazio_DeveLancarArgumentException(string hashInvalido)
    {
        Assert.Throws<ArgumentException>(() => new Senha(hashInvalido));
    }

    [Fact]
    public void Senhas_ComMesmoHash_EqualsDeveSerTrue()
    {
        const string hash = "HASH::MinhaSenha123";
        var senha1 = new Senha(hash);
        var senha2 = new Senha(hash);
        
        Assert.True(senha1.Equals(senha2));
    }
    
    [Fact]
    public void Senhas_ComHashDistintos_EqualsDeveSerFalse()
    {
        var senha1 = new Senha("HASH::MinhaSenha123");
        var senha2 = new Senha("HASH::MinhaSenhaDiferente321");
        
        Assert.False(senha1.Equals(senha2));
    }

    [Fact]
    public void Senhas_ComMesmoHash_GetHashCode_EqualsDeveSerTrue()
    {
        const string hash = "HASH::MinhaSenha123";
        var senha1 = new Senha(hash);
        var senha2 = new Senha(hash);
        
        Assert.Equal(senha1.GetHashCode(), senha2.GetHashCode());
    }

    [Fact]
    public void Senha_ToString_DeveRetornarHash()
    {
        const string hash = "HASH::MinhaSenha123";
        var senha = new Senha(hash);
        
        Assert.Equal(hash, senha.ToString());
    }
}