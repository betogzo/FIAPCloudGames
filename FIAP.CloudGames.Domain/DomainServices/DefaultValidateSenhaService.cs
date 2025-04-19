using System.Text.RegularExpressions;
using FIAP.CloudGames.Domain.Interfaces.Policies;

namespace FIAP.CloudGames.Domain.DomainServices;

public partial class DefaultValidateSenhaService : IValidateSenhaService
{
    private readonly Regex _regexValidation = MyRegex();
    
    public bool IsValid(string senha)
    {
        return (!string.IsNullOrWhiteSpace(senha)) && _regexValidation.IsMatch(senha);
    }

    [GeneratedRegex("^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    private static partial Regex MyRegex();
}