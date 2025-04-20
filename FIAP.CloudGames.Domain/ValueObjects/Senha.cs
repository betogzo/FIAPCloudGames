using FIAP.CloudGames.Domain.Shared;

namespace FIAP.CloudGames.Domain.ValueObjects;

public class Senha : BaseValueObject
{
    public string ValorHash { get; }

    private Senha(string hash)
    {
        ValorHash = hash;
    }

    public static Result<Senha> Create(string hash)
    {
        return string.IsNullOrWhiteSpace(hash)
            ? Result<Senha>.Fail(new List<string> { "Hash da senha não pode ser nulo ou vazio." })
            : Result<Senha>.Ok(new Senha(hash));
    }
    
    public override string ToString() => ValorHash;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ValorHash;
    }
}