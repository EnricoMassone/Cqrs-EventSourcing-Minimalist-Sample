using Inventory.Domain.Exceptions;

namespace Inventory.Domain.ValueObjects
{
  public sealed record InventoryItemId
  {
    public static InventoryItemId FromGuid(Guid value)
    {
      if (value == default)
      {
        throw new InvalidInventoryItemIdException($"Value {value} is not valid for Inventory Item Id");
      }

      return new InventoryItemId(value);
    }

    public Guid Value { get; }

    internal InventoryItemId(Guid value) => this.Value = value;

    public static implicit operator Guid(InventoryItemId inventoryItemId) =>
      inventoryItemId.Value;
  }
}
