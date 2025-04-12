using System.Text.RegularExpressions;

namespace FIAP.CloudGames.Domain.ValueObjects;

public class Email : BaseValueObject
{
    public string Endereco { get; }

    public Email(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("E-mail nao pode ser vazio.");

        if (!Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("E-mail invalido.");
            
        Endereco = endereco;
    }
    
    public override string ToString() => Endereco;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Endereco;
    }
}
