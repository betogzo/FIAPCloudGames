using FIAP.CloudGames.Domain.Entities;
using FIAP.CloudGames.Domain.Enums;
using FIAP.CloudGames.Domain.ValueObjects;

namespace FIAP.CloudGames.Tests.Domain.Entities;

public class UsuarioTests
{
    private readonly string _nomeValido = "Alberto Galeazzo";
    private readonly Email _emailValido = new Email("albertogaleazzo@msn.com");
    private readonly Senha _senhaValida = new Senha("HASH::minha$uperSenha1234");
    
    [Fact]
    public void CriarUsuario_ComDadosValidos_DevePassar()
    { 
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        
        Assert.Equal(_nomeValido, usuario.Nome);
        Assert.Equal(_emailValido, usuario.Email);
        Assert.Equal(_senhaValida, usuario.Senha);
        Assert.Equal(ETipo.Usuario, usuario.Tipo);
        Assert.NotEqual(Guid.Empty, usuario.Id);
        Assert.True(usuario.CreatedAt <= DateTime.UtcNow);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarUsuario_ComNomeNuloOuVazio_DeveLancarArgumentException(string nomeInvalido)
    {
        Assert.Throws<ArgumentException>(
            () => new Usuario(nomeInvalido, _emailValido, _senhaValida));
    }

    [Fact]
    public void CriarUsuario_SemTipoInformado_DeveAssumirTipoUsuario()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        
        Assert.Equal(ETipo.Usuario, usuario.Tipo);
    }

    [Fact]
    public void CriarUsuario_ComEmailNulo_DeveLancarArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => new Usuario(_nomeValido, null, _senhaValida));
    }
    
    [Fact]
    public void CriarUsuario_ComSenhaNulo_DeveLancarArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => new Usuario(_nomeValido, _emailValido, null));
    }
    
    [Fact]
    public void CriarUsuario_ComSucesso_IdNaoDeveSerGuidVazio()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        
        Assert.NotEqual(Guid.Empty, usuario.Id);
    }

    [Fact]
    public void AtualizarNome_ComValorValido_DeveAtualizarNome()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        var novoNome = "Fabiana Artuso";
        
        usuario.AtualizarNome(novoNome);
        
        Assert.Equal(novoNome, usuario.Nome);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AtualizarNome_ComValorNuloOuVazio_DeveLancarArgumentException(string nomeInvalido)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
            usuario.AtualizarNome(nomeInvalido);
        });
    }

    [Fact]
    public void AtualizarEmail_ComValorValido_DeveAtualizarEmail()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        var emailNovoValido = "albertogaleazzo@outlook.com";
        usuario.AtualizarEmail(emailNovoValido);
        
        Assert.Equal(new Email(emailNovoValido), usuario.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AtualizarEmail_ComValorNuloOuVazio_DeveLancarArgumentException(string emailInvalido)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
            usuario.AtualizarEmail(emailInvalido);
        });
    }

    [Fact]
    public void AtualizarSenha_ComValorValido_DeveAtualizarSenha()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        var senhaNova = "HASH::MinhaSenhaNovissima!@#";
        usuario.AtualizarSenha(senhaNova);
        
        Assert.Equal(new Senha(senhaNova), usuario.Senha);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AtualizarSenha_ComValorNuloOuVazio_DeveLancarArgumentException(string senhaInvalida)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
            usuario.AtualizarSenha(senhaInvalida);
        });
    }

    [Fact]
    public void AtualizarSenha_ComHashIgualAoAtual_DeveLancarInvalidOperationException()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        var senhaRepetida = _senhaValida.ValorHash;
    
        Assert.Throws<InvalidOperationException>(() =>
        {
            usuario.AtualizarSenha(senhaRepetida);
        });
    }

    [Fact]
    public void Id_AposAlgumaAtualizacao_NaoPodeMudar()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        var idUsuario = usuario.Id;
        
        usuario.AtualizarNome("Alberto");
        usuario.AtualizarEmail("albertogaleazzo@outlook.com");
        usuario.AtualizarSenha("HASH::MinhaSenhaNovissima!@#");

        Assert.Equal(idUsuario, usuario.Id);
    }
    
    [Fact]
    public void CreatedAt_AposAlgumaAtualizacao_NaoPodeMudar()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida);
        var createdAt = usuario.CreatedAt;
        
        usuario.AtualizarNome("Alberto");
        usuario.AtualizarEmail("albertogaleazzo@outlook.com");
        usuario.AtualizarSenha("HASH::MinhaSenhaNovissima!@#");

        Assert.Equal(createdAt, usuario.CreatedAt);
    }

    [Fact]
    public void PromoverParaAdmin_UsuarioComum_DeveAlterarTipoParaAdministrador()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida, ETipo.Usuario);
        usuario.PromoverParaAdmin();
        
        Assert.True(usuario.Tipo == ETipo.Administrador);
    }
    
    [Fact]
    public void PromoverParaAdmin_UsuarioJaAdministrador_DeveLancarInvalidOperationException()
    {
        var usuario = new Usuario(_nomeValido, _emailValido, _senhaValida, ETipo.Administrador);

        Assert.Throws<InvalidOperationException>(() => usuario.PromoverParaAdmin());
    }
    
    [Fact]
    public void MetodoEquals_UsuariosComMesmoId_DeveSerTrue()
    {
        var u1 = new Usuario("Nome A", new Email("a@a.com"), new Senha("HASH::a"));
        var u2 = new Usuario("Nome B", new Email("b@b.com"), new Senha("HASH::b"));
    
        typeof(Usuario).GetProperty("Id")!
            .SetValue(u2, u1.Id); //uso de reflection a bem da ciência
    
        Assert.True(u1.Equals(u2));
    }
}