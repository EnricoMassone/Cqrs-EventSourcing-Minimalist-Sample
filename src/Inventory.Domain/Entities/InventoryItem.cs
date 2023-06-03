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
    public InventoryItemName Name { get; private set; }
    public Quantity CurrentQuantity { get; private set; }
    public Quantity MaximumAllowedQuantity { get; private set; }

    public InventoryItem(
      InventoryItemId id,
      InventoryItemName name,
      Quantity? currentQuantity = default,
      Quantity? maximumAllowedQuantity = default)
    {
      if (id is null)
      {
        throw new ArgumentNullException(nameof(id));
      }

      if (name is null)
      {
        throw new ArgumentNullException(nameof(name));
      }

      var @event = new Events.InventoryItemCreated
      {
        Id = id,
        Name = name,
        CurrentQuantity = currentQuantity ?? s_defaultInitialQuantity,
        MaximumAllowedQuantity = maximumAllowedQuantity ?? s_defaultMaximumAllowedQuantity,
      };
      this.RaiseEvent(@event);
    }

    protected override void ApplyToState(object @event)
    {
      switch (@event)
      {
        case Events.InventoryItemCreated e:
          this.Id = new InventoryItemId(e.Id);
          this.IsActive = true;
          this.Name = new InventoryItemName(e.Name);
          this.CurrentQuantity = new Quantity(e.CurrentQuantity);
          this.MaximumAllowedQuantity = new Quantity(e.MaximumAllowedQuantity);
          break;
      }
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
