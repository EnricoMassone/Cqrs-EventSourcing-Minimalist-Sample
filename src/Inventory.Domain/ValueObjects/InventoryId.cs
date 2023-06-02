using Inventory.Domain.Exceptions;

namespace Inventory.Domain.ValueObjects
{
  public sealed record InventoryId
  {
    public static InventoryId FromGuid(Guid value)
    {
      if (value == default)
      {
        throw new InvalidInventoryIdException($"Value {value} is not valid for Inventory Id");
      }

      return new InventoryId(value);
    }

    public Guid Value { get; }

    internal InventoryId(Guid value) => this.Value = value;
  }
}
