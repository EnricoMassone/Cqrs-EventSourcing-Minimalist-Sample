namespace Inventory.Domain.ValueObjects
{
  public sealed record Quantity : IComparable<Quantity>
  {
    public static Quantity FromInteger(int value)
    {
      if (value <= 0)
      {
        throw new ArgumentOutOfRangeException(
          nameof(value),
          $"{value} is not a valid quantity. Quantity must be positive integer number.");
      }

      return new Quantity(value);
    }

    public int Value { get; }

    internal Quantity(int value) => this.Value = value;

    #region Interface implementations

    public int CompareTo(Quantity? other)
    {
      if (other == null)
      {
        // see https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1.compareto?view=net-7.0#remarks
        return 1;
      }

      return this.Value.CompareTo(other.Value);
    }

    #endregion

    #region Operator overloads

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static bool operator >(Quantity quantity1, Quantity quantity2)
    {
      return quantity1.CompareTo(quantity2) > 0;
    }

    public static bool operator <(Quantity quantity1, Quantity quantity2)
    {
      return quantity1.CompareTo(quantity2) < 0;
    }

    public static bool operator >=(Quantity quantity1, Quantity quantity2)
    {
      return quantity1.CompareTo(quantity2) >= 0;
    }

    public static bool operator <=(Quantity quantity1, Quantity quantity2)
    {
      return quantity1.CompareTo(quantity2) <= 0;
    }

    #endregion
  }
}
