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
      throw new NotImplementedException();
    }
  }
}
