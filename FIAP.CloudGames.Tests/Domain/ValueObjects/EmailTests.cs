using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void CriarEmail_ComEnderecoValido_DeveArmazenarEndereco()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email = new Email(endereco);

        Assert.Equal(endereco, email.Endereco);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarEmail_ComEnderecoNuloOuVazio_DeveLancarArgumentException(string endereco)
    {
        Assert.Throws<ArgumentException>(() => new Email(endereco));
    }

    [Theory]
    [InlineData("albertogaleazzo")]
    [InlineData("@msn.com")]
    [InlineData("albertogaleazzo.com")]
    [InlineData("albertogaleazzo@msn")]
    public void CriarEmail_ComEnderecoInvalido_DeveLancarArgumentException(string endereco)
    {
        Assert.Throws<ArgumentException>(
            () => new Email(endereco));
    }

    [Fact]
    public void Emails_ComMesmoEndereco_EqualsDeveSerTrue()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email1 = new Email(endereco);
        var email2 = new Email(endereco);
        
        Assert.True(email1.Equals(email2));
    }
    
    [Fact]
    public void Emails_ComEnderecosDistintos_EqualsDeveSerFalse()
    {
        var email1 = new Email("albertogaleazzo@msn.com");
        var email2 = new Email("albertogaleazzo@outlook.com");
        
        Assert.False(email1.Equals(email2));
    }

    [Fact]
    public void Emails_ComMesmoEndereco_DevemTerMesmoHash()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email1 = new Email(endereco);
        var email2 = new Email(endereco);
        
        Assert.Equal(email1.GetHashCode(), email2.GetHashCode());
    }

    [Fact]
    public void Email_ToString_DeveRetornarEndereco()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email = new Email(endereco);
        
        Assert.Equal(endereco, email.ToString());
    }
}