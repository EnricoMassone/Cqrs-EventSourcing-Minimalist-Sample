using Inventory.Domain.Exceptions;
using Inventory.Domain.Framework;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities
{
  public sealed class InventoryItem : AggregateRoot<InventoryItemId>
  {
    private static readonly Quantity s_defaultInitialQuantity = Quantity.FromInteger(1);
    private static readonly Quantity s_defaultMaximumAllowedQuantity = Quantity.FromInteger(5);

    public bool IsActive { get; private set; }
    public string Name { get; private set; }
    public Quantity CurrentQuantity { get; private set; }
    public Quantity MaximumAllowedQuantity { get; private set; }

    public InventoryItem(
      InventoryItemId id,
      string name,
      Quantity? currentQuantity = default,
      Quantity? maximumAllowedQuantity = default)
    {
      throw new NotImplementedException();
    }

    protected override void ApplyToState(object @event)
    {
      throw new NotImplementedException();
    }

    protected override void EnsureValidState()
    {
      var isValid = this.CurrentQuantity <= this.MaximumAllowedQuantity;

      if (isValid)
      {
        return;
      }

      throw NewInvalidEntityStateException();

      InvalidEntityStateException NewInvalidEntityStateException()
      {
        var message =
          $"State change for entity of type {nameof(InventoryItemId)} with Id {this.Id} has been rejected. "
          +
          "Current quantity must be less than or equal to maximum allowed quantity. "
          +
          "Inventory item name cannot be null or white space.";

        return new InvalidEntityStateException(message);
      }
    }
  }
}
