using Inventory.Domain.Exceptions;

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
        throw new InvalidQuantityException($"An instance of {nameof(NonZeroQuantity)} must represent a positive (non zero) quantity");
      }
    }

    new public static NonZeroQuantity FromInteger(int value) =>
      new(Quantity.FromInteger(value));
  }
}
