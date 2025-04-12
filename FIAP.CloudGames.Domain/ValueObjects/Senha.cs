namespace FIAP.CloudGames.Domain.ValueObjects;

public class Senha : BaseValueObject
{
    public string ValorHash { get; }

    public Senha(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash da senha não pode ser vazio.");

        ValorHash = hash;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ValorHash;
    }
}