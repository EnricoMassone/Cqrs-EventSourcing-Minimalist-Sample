namespace Inventory.Domain.ValueObjects
{
  public sealed record NonZeroQuantity : Quantity
  {
    internal NonZeroQuantity(int value) : base(value)
    {
    }

    private NonZeroQuantity(Quantity quantity) : base(quantity)
    {
      if (this.Value == 0)
      {
        throw new ArgumentOutOfRangeException(
          "value",
          $"Value of 0 is invalid for an instance of {nameof(NonZeroQuantity)}. Value must be positive integer number.");
      }
    }

    new public static NonZeroQuantity FromInteger(int value) =>
      new(Quantity.FromInteger(value));
  }
}
