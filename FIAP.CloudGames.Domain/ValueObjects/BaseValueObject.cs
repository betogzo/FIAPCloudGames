namespace FIAP.CloudGames.Domain.ValueObjects;

public abstract class BaseValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is not BaseValueObject other || GetType() != other.GetType())
            return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (hash, obj) => hash * 23 + (obj?.GetHashCode() ?? 0));
    }

    public static bool operator ==(BaseValueObject? a, BaseValueObject? b)
        => a is null && b is null || a is not null && a.Equals(b);

    public static bool operator !=(BaseValueObject? a, BaseValueObject? b)
        => !(a == b);
}