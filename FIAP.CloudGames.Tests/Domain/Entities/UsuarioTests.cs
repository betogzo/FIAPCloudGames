using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.Entities;

public class UsuarioTests
{
    private readonly string _nomeValido = "Alberto Galeazzo";
    private readonly string _emailValido = "albertogaleazzo@msn.com";
    private readonly string _senhaValida = "HASH::minha$uperSenha1234";

    [Fact]
    public void CriarUsuario_ComDadosValidos_DevePassar()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);

        Assert.True(usuario.IsSuccess);
        Assert.IsType<Usuario>(usuario.Data);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarUsuario_ComNomeNuloOuVazio_DeveResultarFalha(string nomeInvalido)
    {
        var usuario = Usuario.Create(nomeInvalido, _emailValido, _senhaValida);

        Assert.False(usuario.IsSuccess);
        Assert.Contains("nome", usuario.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CriarUsuario_SemTipoInformado_DeveAssumirTipoUsuario()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);

        Assert.True(usuario.IsSuccess);
        Assert.Equal(ETipo.Usuario, usuario.Data.Tipo);
    }

    [Fact]
    public void CriarUsuario_ComEmailNulo_DeveResultarFalha()
    {
        var usuario = Usuario.Create(_nomeValido, null, _senhaValida);

        Assert.False(usuario.IsSuccess);
        Assert.Contains("e-mail", usuario.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CriarUsuario_ComSenhaNulo_DeveResultarFalha()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, null);

        Assert.False(usuario.IsSuccess);
        Assert.Contains("senha", usuario.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CriarUsuario_ComSucesso_IdNaoDeveSerGuidVazio()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);

        Assert.True(usuario.IsSuccess);
        Assert.NotEqual(Guid.Empty, usuario.Data.Id);
    }

    [Fact]
    public void AtualizarNome_ComValorValido_DeveAtualizarNome()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var novoNome = "Fabiana Artuso";

        usuario.Data.AtualizarNome(novoNome);
        Assert.Equal(novoNome, usuario.Data.Nome);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AtualizarNome_ComValorNuloOuVazio_DeveResultarFalha(string nomeInvalido)
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var resultadoAtualizarNome = usuario.Data.AtualizarNome(nomeInvalido);

        Assert.False(resultadoAtualizarNome.IsSuccess);
        Assert.Contains("nome", resultadoAtualizarNome.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AtualizarEmail_ComValorValido_DeveAtualizarEmail()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var emailNovoValido = "albertogaleazzo@outlook.com";
        var resultadoAtualizarEmail = usuario.Data.AtualizarEmail(emailNovoValido);

        Assert.True(resultadoAtualizarEmail.IsSuccess);
        Assert.Equal(emailNovoValido, usuario.Data.Email.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AtualizarEmail_ComValorNuloOuVazio_DeveResultarFalha(string emailInvalido)
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var resultadoAtualizarEmail = usuario.Data.AtualizarEmail(emailInvalido);

        Assert.False(resultadoAtualizarEmail.IsSuccess);
    }

    [Fact]
    public void AtualizarSenha_ComValorValido_DeveAtualizarSenha()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var senhaNova = "HASH::MinhaSenhaNovissima!@#";
        var resultadoAtualizarSenha = usuario.Data.AtualizarSenha(senhaNova);

        Assert.True(resultadoAtualizarSenha.IsSuccess);
        Assert.Equal(Senha.Create(senhaNova).Data, usuario.Data.Senha);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AtualizarSenha_ComValorNuloOuVazio_DeveResultarFalha(string senhaInvalida)
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var resultadoAtualizarSenha = usuario.Data.AtualizarSenha(senhaInvalida);

        Assert.False(resultadoAtualizarSenha.IsSuccess);
        Assert.Contains("senha", resultadoAtualizarSenha.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AtualizarSenha_ComHashIgualAoAtual_DeveResultarFalha()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var resultadoAtualizarSenha = usuario.Data.AtualizarSenha(_senhaValida);

        Assert.False(resultadoAtualizarSenha.IsSuccess);
        Assert.Contains("anterior", resultadoAtualizarSenha.Errors[0], StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Id_AposAlgumaAtualizacao_NaoPodeMudar()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var idUsuario = usuario.Data.Id;

        var resultados = new[]
        {
            usuario.Data.AtualizarNome("Alberto"),
            usuario.Data.AtualizarEmail("albertogaleazzo@outlook.com"),
            usuario.Data.AtualizarSenha("HASH::MinhaSenhaNovissima!@#")
        };

        Assert.All(resultados, r => Assert.True(r.IsSuccess));

        Assert.Equal(idUsuario, usuario.Data.Id);
    }

    [Fact]
    public void CreatedAt_AposAlgumaAtualizacao_NaoPodeMudar()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);

        var createdAt = usuario.Data.CreatedAt;

        var resultados = new[]
        {
            usuario.Data.AtualizarNome("Alberto"),
            usuario.Data.AtualizarEmail("albertogaleazzo@outlook.com"),
            usuario.Data.AtualizarSenha("HASH::MinhaSenhaNovissima!@#")
        };

        Assert.All(resultados, r => Assert.True(r.IsSuccess));

        Assert.Equal(createdAt, usuario.Data.CreatedAt);
    }

    [Fact]
    public void UpdatedAt_AposAlgumaAtualizacao_DeveMudar()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida);
        Assert.True(usuario.IsSuccess);
        
        var updatedAtInicial = usuario.Data.UpdatedAt;
        
        var resultadoAtualizarNome = usuario.Data.AtualizarNome("Alberto");
        Assert.True(resultadoAtualizarNome.IsSuccess);
        
        Assert.True(updatedAtInicial < usuario.Data.UpdatedAt );
    }


    [Fact]
    public void PromoverParaAdmin_UsuarioComum_DeveAlterarTipoParaAdministrador()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida, ETipo.Usuario);
        Assert.True(usuario.IsSuccess);

        var resultadoPromoverParaAdmin = usuario.Data.PromoverParaAdmin();

        Assert.True(resultadoPromoverParaAdmin.IsSuccess);
        Assert.True(usuario.Data.Tipo == ETipo.Administrador);
    }
    

    [Fact]
    public void PromoverParaAdmin_UsuarioJaAdministrador_DeveResultarFalha()
    {
        var usuario = Usuario.Create(_nomeValido, _emailValido, _senhaValida, ETipo.Administrador);
        Assert.True(usuario.IsSuccess);

        var resultadoPromoverParaAdmin = usuario.Data.PromoverParaAdmin();
        
        Assert.False(resultadoPromoverParaAdmin.IsSuccess);
        Assert.Contains("Administrador", resultadoPromoverParaAdmin.Errors[0], StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public void MetodoEquals_UsuariosComMesmoId_DeveSerTrue()
    {
        var u1 = Usuario.Create("Nome A", "a@a.com", "HASH::a");
        var u2 = Usuario.Create("Nome B", "b@b.com", "HASH::b");
    
        typeof(Usuario).GetProperty("Id")!
            .SetValue(u2.Data, u1.Data.Id); //uso de reflection a bem da ciência
    
        Assert.True(u1.Data.Equals(u2.Data));
    }
}