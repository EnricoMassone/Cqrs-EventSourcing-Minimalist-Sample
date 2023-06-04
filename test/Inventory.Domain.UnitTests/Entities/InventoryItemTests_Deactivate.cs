namespace Inventory.Domain.UnitTests.Entities
{
  [TestFixture]
  public sealed partial class InventoryItemTests
  {
    [Test]
    public void Deactivate_Sets_IsActive_Property_To_False()
    {
      // ARRANGE
      var target = CreateTestTarget();

      // ACT
      target.Deactivate();

      // ASSERT
      Assert.That(target.IsActive, Is.False);

      // check events
      var changes = target.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Has.Count.EqualTo(1));
      Assert.That(changes[0], Is.InstanceOf<Events.InventoryItemDeactivated>());

      var @event = (Events.InventoryItemDeactivated)changes[0];

      Assert.That(@event.Id, Is.EqualTo(target.Id.Value));
    }

    [Test]
    public void Deactivate_Does_Not_Raise_Event_When_Inventory_Item_Is_Not_Active()
    {
      // ARRANGE
      var target = CreateTestTarget();

      target.Deactivate();

      target.ClearChanges();

      // ACT
      target.Deactivate();

      // ASSERT
      Assert.That(target.IsActive, Is.False);

      // check events
      var changes = target.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Is.Empty);
    }
  }
}
