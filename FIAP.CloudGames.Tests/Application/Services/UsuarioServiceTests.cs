using FIAP.CloudGames.Application.DTOs.Request.Usuario;
using FIAP.CloudGames.Application.Services;
using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Interfaces.Policies;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Application.Interfaces.Services;
using Moq;

namespace FIAP.CloudGames.Tests.Application.Services;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly Mock<IValidateSenhaService> _mockValidateSenhaService;
    private readonly Mock<ICriptografiaService> _mockCriptografiaService;
    private readonly UsuarioService _service;

    public UsuarioServiceTests()
    {
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockValidateSenhaService = new Mock<IValidateSenhaService>();
        _mockCriptografiaService = new Mock<ICriptografiaService>();
        _service = new UsuarioService(
            _mockUsuarioRepository.Object,
            _mockValidateSenhaService.Object,
            _mockCriptografiaService.Object
        );
    }

    [Fact]
    public async Task RegistrarAsync_EmailJaExiste_DeveResultarFalha()
    {
        _mockUsuarioRepository
            .Setup(r => r.AlreadyExistsByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(true);

        var result = await _service.RegistrarAsync(
            new RegistrarUsuarioDto(
                "Alberto Galeazzo",
                "albertogaleazzo@msn.com",
                "Senha@12345")
        );

        Assert.False(result.IsSuccess);
        Assert.Equal("Endereco de e-mail ja cadastrado.", result.Errors[0]);
    }

    [Theory]
    [InlineData("1223423423423434@")]
    [InlineData("12@ffw")]
    [InlineData("asdfsdfsdfsdfsdf@")]
    [InlineData("!@#%!@%^#@#@!")]
    public async Task RegistrarAsync_SenhaFraca_DeveResultarFalha(string senhaInvalida)
    {
        _mockUsuarioRepository
            .Setup(r => r.AlreadyExistsByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        _mockCriptografiaService
            .Setup(c => c.HashSenha(It.IsAny<string>()))
            .Returns("HASH::SenhaFicticia");

        var result = await _service.RegistrarAsync(
            new RegistrarUsuarioDto(
                "Alberto Galeazzo",
                "albertogaleazzo@msn.com",
                senhaInvalida)
        );

        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Contains("fraca", result.Errors[0], StringComparison.CurrentCultureIgnoreCase);
    }

    [Fact]
    public async Task RegistrarAsync_NomeEmailESenhaInvalidos_DeveResultarFalhaComErrosDeDominio()
    {
        _mockUsuarioRepository
            .Setup(r => r.AlreadyExistsByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        _mockCriptografiaService
            .Setup(c => c.HashSenha(It.IsAny<string>()))
            .Returns("HASH::SenhaFicticia");

        var result = await _service.RegistrarAsync(
            new RegistrarUsuarioDto(
                "",
                "albertogaleazzo@msncom",
                "12345$")
        );

        Assert.False(result.IsSuccess);
        Assert.Equal(3, result.Errors.Count);
    }

    [Fact]
    public async Task RegistrarAsync_DadosValidos_DeveResultarSucessoEAdicionarUsuario()
    {
        _mockUsuarioRepository
            .Setup(r => r.AlreadyExistsByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        _mockValidateSenhaService
            .Setup(v => v.IsValid(It.IsAny<string>()))
            .Returns(true);
        
        _mockCriptografiaService
            .Setup(c => c.HashSenha(It.IsAny<string>()))
            .Returns("HASH::SenhaFicticia");
        
        var result = await _service.RegistrarAsync(
            new RegistrarUsuarioDto(
                "Alberto Galeazzo",
                "albertogaleazzo@msn.com",
                "aAbcde@1234")
        );

        Assert.True(result.IsSuccess);
        Assert.IsType<Guid>(result.Data);
    }

    // ATUALIZAR

    [Fact]
    public async Task AtualizarAsync_ComNomeEEmailNovosValidos_DeveResultarSucesso()
    {
        var usuarioValido =
            Usuario.Create("Alberto Galeazzo",
                "albertogaleazzo@msn.com",
                "HASH:MinhaSenha123!");
        
        Assert.True(usuarioValido.IsSuccess);

        _mockUsuarioRepository
            .Setup(r => r.FindByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(usuarioValido.Data);
        
        _mockUsuarioRepository
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        const string novoNome = "Alberto Galeazzo 2";
        const string novoEmail = "albertoatualizado@outlook.com";
        
        var dto = new AtualizarUsuarioDto
        {
            Nome = novoNome,
            Email = novoEmail
        };
        
        var resultado = await _service.AtualizarAsync(Guid.NewGuid(), dto);
        
        Assert.True(resultado.IsSuccess);
        Assert.Equal(novoNome, usuarioValido.Data.Nome);
        Assert.Equal(novoEmail, usuarioValido.Data.Email.ToString());
    }
}