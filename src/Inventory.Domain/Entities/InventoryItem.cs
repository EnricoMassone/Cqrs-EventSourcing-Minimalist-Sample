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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public InventoryItem(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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

    public void ChangeName(InventoryItemName newName)
    {
      if (newName is null)
      {
        throw new ArgumentNullException(nameof(newName));
      }

      if (this.Name.Equals(newName))
      {
        return;
      }

      var @event = new Events.InventoryItemNameChanged
      {
        Id = this.Id,
        NewName = newName,
      };

      this.RaiseEvent(@event);
    }

    public void Deactivate()
    {
      if (!this.IsActive)
      {
        return;
      }

      var @event = new Events.InventoryItemDeactivated
      {
        Id = this.Id
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

        case Events.InventoryItemNameChanged e:
          this.Name = new InventoryItemName(e.NewName);
          break;

        case Events.InventoryItemDeactivated:
          this.IsActive = false;
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
          $"State change for entity of type {nameof(InventoryItem)} with Id {this.Id.Value} has been rejected. "
          +
          "Current quantity must be less than or equal to maximum allowed quantity.";

        return new InvalidEntityStateException(message);
      }
    }
  }
}
