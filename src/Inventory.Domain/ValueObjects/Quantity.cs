namespace Inventory.Domain.ValueObjects
{
  public sealed record Quantity
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

    public static implicit operator int(Quantity quantity) => quantity.Value;
  }
}
