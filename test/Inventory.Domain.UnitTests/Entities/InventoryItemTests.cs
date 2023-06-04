using Inventory.Domain.Entities;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.Entities
{
  [TestFixture]
  public sealed partial class InventoryItemTests
  {
    private static readonly InventoryItemId s_defaultId = InventoryItemId.FromGuid(Guid.NewGuid());
    private static readonly InventoryItemName s_defaultName = InventoryItemName.FromString("Special Item");
    private static readonly Quantity s_defaultCurrentQuantity = Quantity.FromInteger(3);
    private static readonly Quantity s_defaultMaximumAllowedQuantity = Quantity.FromInteger(10);

    private static InventoryItem CreateTestTarget(
      InventoryItemId? id = default,
      InventoryItemName? name = default,
      Quantity? currentQuantity = default,
      Quantity? maximumAllowedQuantity = default)
    {
      var result = new InventoryItem(
        id ?? s_defaultId,
        name ?? s_defaultName,
        currentQuantity ?? s_defaultCurrentQuantity,
        maximumAllowedQuantity ?? s_defaultMaximumAllowedQuantity);

      result.ClearChanges();

      return result;
    }
  }
}
