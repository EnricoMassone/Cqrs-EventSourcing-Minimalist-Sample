using Inventory.Domain.Exceptions;
using Inventory.Domain.Framework;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.Entities
{
  public sealed class InventoryItem : AggregateRoot<InventoryItemId>
  {
    public bool IsActive { get; private set; }
    public Quantity CurrentQuantity { get; private set; }
    public Quantity MaximumAllowedQuantity { get; private set; }

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
          "Current quantity must be less than or equal to maximum allowed quantity.";

        return new InvalidEntityStateException(message);
      }
    }
  }
}
