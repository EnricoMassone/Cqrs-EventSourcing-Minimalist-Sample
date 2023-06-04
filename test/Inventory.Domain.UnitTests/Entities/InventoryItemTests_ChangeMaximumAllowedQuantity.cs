using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.Entities
{
  [TestFixture]
  public sealed partial class InventoryItemTests
  {
    [Test]
    public void ChangeMaximumAllowedQuantity_Throws_ArgumentNullException_When_NewMaximumAllowedQuantity_Is_Null()
    {
      // ARRANGE
      var target = CreateTestTarget();

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => target.ChangeMaximumAllowedQuantity(null!)
      );

      // ASSERT
      Assert.That(exception.ParamName, Is.EqualTo("newMaximumAllowedQuantity"));
    }

    [Test]
    public void ChangeMaximumAllowedQuantity_Does_Not_Raise_Event_When_New_Maximum_Allowed_Quantity_Equals_Current_Maximum_Allowed_Quantity()
    {
      // ARRANGE
      const int maximumAllowedQuantity = 10;

      var target = CreateTestTarget(
        currentQuantity: Quantity.FromInteger(2),
        maximumAllowedQuantity: Quantity.FromInteger(maximumAllowedQuantity)
      );

      // ACT
      target.ChangeMaximumAllowedQuantity(Quantity.FromInteger(maximumAllowedQuantity));

      // ASSERT
      Assert.That(target.MaximumAllowedQuantity, Is.EqualTo(Quantity.FromInteger(maximumAllowedQuantity)));

      // check events
      var changes = target.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Is.Empty);
    }

    [TestCase(2, 6, 9)]
    [TestCase(2, 6, 4)]
    [TestCase(2, 6, 2)]
    public void ChangeMaximumAllowedQuantity_Changes_Maximum_Allowed_Quantity_Of_Inventory_Item(
      int currentQuantity,
      int oldMaximumAllowedQuantity,
      int newMaximumAllowedQuantity)
    {
      // ARRANGE
      var target = CreateTestTarget(
        currentQuantity: Quantity.FromInteger(currentQuantity),
        maximumAllowedQuantity: Quantity.FromInteger(oldMaximumAllowedQuantity)
      );

      // ACT
      target.ChangeMaximumAllowedQuantity(Quantity.FromInteger(newMaximumAllowedQuantity));

      // ASSERT
      Assert.That(target.MaximumAllowedQuantity, Is.EqualTo(Quantity.FromInteger(newMaximumAllowedQuantity)));

      // check events
      var changes = target.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Has.Count.EqualTo(1));
      Assert.That(changes[0], Is.InstanceOf<Events.InventoryItemMaximumAllowedQuantityChanged>());

      var @event = (Events.InventoryItemMaximumAllowedQuantityChanged)changes[0];

      Assert.That(@event.Id, Is.EqualTo(target.Id.Value));
      Assert.That(@event.NewMaximumAllowedQuantity, Is.EqualTo(newMaximumAllowedQuantity));
    }

    [Test]
    public void ChangeMaximumAllowedQuantity_Throws_InvalidEntityStateException_When_New_Maximum_Allowed_Quantity_Is_Lower_Than_Current_Quantity_Of_Inventory_Item()
    {
      // ARRANGE
      var target = CreateTestTarget(
        id: InventoryItemId.FromGuid(Guid.Parse("20a803bf-8f80-4c34-abba-c81032a69191")),
        currentQuantity: Quantity.FromInteger(6),
        maximumAllowedQuantity: Quantity.FromInteger(10)
      );

      // ACT
      var exception = Assert.Throws<InvalidEntityStateException>(
        () => target.ChangeMaximumAllowedQuantity(Quantity.FromInteger(5))
      );

      // ASSERT
      const string expectedMessage =
        "State change for entity of type InventoryItem with Id 20a803bf-8f80-4c34-abba-c81032a69191 has been rejected. "
          +
        "Current quantity must be less than or equal to maximum allowed quantity.";

      Assert.That(
        exception.Message,
        Is.EqualTo(expectedMessage)
      );
    }
  }
}
