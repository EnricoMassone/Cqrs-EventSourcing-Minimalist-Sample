using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.Entities
{
  [TestFixture]
  public sealed partial class InventoryItemTests
  {
    [Test]
    public void ChangeName_Throws_ArgumentNullException_When_NewName_Is_Null()
    {
      // ARRANGE
      var target = CreateTestTarget();

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => target.ChangeName(null!)
      );

      // ASSERT
      Assert.That(exception.ParamName, Is.EqualTo("newName"));
    }

    [Test]
    public void ChangeName_Does_Not_Raise_Event_When_New_Name_Equals_Current_Name()
    {
      // ARRANGE
      const string name = "Blue Bike";

      var target = CreateTestTarget(
        name: InventoryItemName.FromString(name)
      );

      // ACT
      target.ChangeName(InventoryItemName.FromString(name));

      // ASSERT
      Assert.That(target.Name, Is.EqualTo(InventoryItemName.FromString(name)));

      // check events
      var changes = target.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Is.Empty);
    }

    [Test]
    public void ChangeName_Changes_Name_Of_Inventory_Item()
    {
      // ARRANGE
      const string oldName = "Blue Bike";

      var target = CreateTestTarget(
        name: InventoryItemName.FromString(oldName)
      );

      // ACT
      const string newName = "Super Item 123";

      target.ChangeName(InventoryItemName.FromString(newName));

      // ASSERT
      Assert.That(target.Name, Is.EqualTo(InventoryItemName.FromString(newName)));

      // check events
      var changes = target.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Has.Count.EqualTo(1));
      Assert.That(changes[0], Is.InstanceOf<Events.InventoryItemNameChanged>());

      var @event = (Events.InventoryItemNameChanged)changes[0];

      Assert.That(@event.Id, Is.EqualTo(target.Id.Value));
      Assert.That(@event.NewName, Is.EqualTo(newName));
    }
  }
}
