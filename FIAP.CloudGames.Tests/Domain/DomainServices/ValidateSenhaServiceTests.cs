using FIAP.CloudGames.Domain.DomainServices;

namespace FIAP.CloudGames.Tests.Domain.DomainServices;

public class ValidateSenhaServiceTests
{
    private readonly DefaultValidateSenhaService _validateSenhaService = new DefaultValidateSenhaService();

    [Fact]
    public void IsValid_SenhaComTodosOsRequisitos_DeveRetornarTrue()
    {
        const string senhaValida = "abcde@#12345!";
        var isValid = _validateSenhaService.IsValid(senhaValida);
        
        Assert.True(isValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void IsValid_SenhaNulaOuVazia_DeveRetornarFalse(string senha)
    {
        var isValid = _validateSenhaService.IsValid(senha);
        
        Assert.False(isValid);
    }
    
    [Fact]
    public void IsValid_ComMenosDe8Caracteres_DeveRetornarFalse()
    {
        const string senhaInvalida = "123";
        
        var isValid = _validateSenhaService.IsValid(senhaInvalida);
        
        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_SemLetras_DeveRetornarFalse()
    {
        const string senhaInvalida = "@$123456789";
        var isValid = _validateSenhaService.IsValid(senhaInvalida);
        
        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_SemNumeros_DeveRetornarFalse()
    {
        const string senhaInvalida = "abcdefg!@#";
        var isValid = _validateSenhaService.IsValid(senhaInvalida);
        
        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_SemCaractereEspecial_DeveRetornarFalse()
    {
        const string senhaInvalida = "abcde12345678";
        var isValid = _validateSenhaService.IsValid(senhaInvalida);
        
        Assert.False(isValid);
    }
}