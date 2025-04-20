using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void CriarEmail_ComEnderecoValido_DeveArmazenarEndereco()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email = Email.Create(endereco);

        Assert.True(email.IsSuccess);
        Assert.Equal(endereco, email.Data.Endereco);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarEmail_ComEnderecoNuloOuVazio_DeveResultarFalha(string endereco)
    {
        var email = Email.Create(endereco);
        Assert.False(email.IsSuccess);
        Assert.Contains("vazio", email.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("albertogaleazzo")]
    [InlineData("@msn.com")]
    [InlineData("albertogaleazzo.com")]
    [InlineData("albertogaleazzo@msn")]
    public void CriarEmail_ComEnderecoInvalido_DeveLancarArgumentException(string endereco)
    {
        var email = Email.Create(endereco);
        Assert.False(email.IsSuccess);
        Assert.Contains("inválido", email.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Emails_ComMesmoEndereco_EqualsDeveSerTrue()
    {
        const string endereco = "albertogaleazzo@msn.com";

        var email1 = Email.Create(endereco);
        Assert.True(email1.IsSuccess);
        var email2 = Email.Create(endereco);
        Assert.True(email2.IsSuccess);

        Assert.True(email1.Data.Equals(email2.Data));
    }

    [Fact]
    public void Emails_ComEnderecosDistintos_EqualsDeveSerFalse()
    {
        var email1 = Email.Create("albertogaleazzo@msn.com");
        Assert.True(email1.IsSuccess);
        var email2 = Email.Create("albertogaleazzo@outlook.com");
        Assert.True(email2.IsSuccess);

        Assert.False(email1.Data.Equals(email2.Data));
    }

    [Fact]
    public void Emails_ComMesmoEndereco_DevemTerMesmoHash()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email1 = Email.Create(endereco);
        Assert.True(email1.IsSuccess);
        var email2 = Email.Create(endereco);
        Assert.True(email2.IsSuccess);

        Assert.Equal(email1.Data.GetHashCode(), email2.Data.GetHashCode());
    }

    [Fact]
    public void Email_ToString_DeveRetornarEndereco()
    {
        const string endereco = "albertogaleazzo@msn.com";
        var email = Email.Create(endereco);

        Assert.Equal(endereco, email.Data.ToString());
    }
}