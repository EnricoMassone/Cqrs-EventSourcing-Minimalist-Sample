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

    public int CompareTo(Quantity? other)
    {
      if (other == null)
      {
        // see https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1.compareto?view=net-7.0#remarks
        return 1;
      }

      return this.Value.CompareTo(other.Value);
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;
  }
}
