using System.Text.RegularExpressions;
using FIAP.CloudGames.Domain.Shared;

namespace FIAP.CloudGames.Domain.ValueObjects;

public class Email : BaseValueObject
{
    public string Endereco { get; }

    private Email(string endereco)
    {
        Endereco = endereco;
    }

    public static Result<Email> Create(string endereco)
    {
        var listaErros = new List<string>();

        if (string.IsNullOrWhiteSpace(endereco))
            listaErros.Add("Endereco de e-mail não pode ser nulo ou vazio.");
        else if (!Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            listaErros.Add(
                "Endereco de e-mail inválido.");

        return listaErros.Any() ? Result<Email>.Fail(listaErros) : Result<Email>.Ok(new Email(endereco));
    }

    public override string ToString() => Endereco;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Endereco;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Email other)
            return false;

        return Endereco == other.Endereco;
    }
}