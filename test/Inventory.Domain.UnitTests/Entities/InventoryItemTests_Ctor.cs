using Inventory.Domain.Entities;
using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.Entities
{
  [TestFixture]
  public sealed partial class InventoryItemTests
  {
    [Test]
    public void Ctor_Throws_ArgumentNullException_When_Id_Is_Null()
    {
      // ARRANGE
      var name = InventoryItemName.FromString("Car");
      var currentQuantity = Quantity.FromInteger(2);
      var maximumAllowedQuantity = Quantity.FromInteger(5);

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(() => new InventoryItem(
        id: null!,
        name,
        currentQuantity,
        maximumAllowedQuantity)
      );

      // ASSERT
      Assert.That(exception.ParamName, Is.EqualTo("id"));
    }

    [Test]
    public void Ctor_Throws_ArgumentNullException_When_Name_Is_Null()
    {
      // ARRANGE
      var id = InventoryItemId.FromGuid(Guid.NewGuid());
      var currentQuantity = Quantity.FromInteger(2);
      var maximumAllowedQuantity = Quantity.FromInteger(5);

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(() => new InventoryItem(
        id,
        name: null!,
        currentQuantity,
        maximumAllowedQuantity)
      );

      // ASSERT
      Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    [Test]
    public void Ctor_Allows_To_Create_New_Instance_With_Default_Values_For_Current_Quantity_And_Maximum_Allowed_Quantity()
    {
      // ARRANGE
      var id = InventoryItemId.FromGuid(Guid.NewGuid());
      var name = InventoryItemName.FromString("Car");

      // ACT
      var result = new InventoryItem(id, name);

      // ASSERT
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Id, Is.EqualTo(id));
      Assert.That(result.Name, Is.EqualTo(name));
      Assert.That(result.IsActive, Is.True);

      var expectedCurrentQuantity = Quantity.FromInteger(1);
      Assert.That(result.CurrentQuantity, Is.EqualTo(expectedCurrentQuantity));

      var expectedMaximumAllowedQuantity = Quantity.FromInteger(5);
      Assert.That(result.MaximumAllowedQuantity, Is.EqualTo(expectedMaximumAllowedQuantity));

      // check events
      var changes = result.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Has.Count.EqualTo(1));
      Assert.That(changes[0], Is.InstanceOf<Events.InventoryItemCreated>());

      var @event = (Events.InventoryItemCreated)changes[0];

      Assert.That(@event.Id, Is.EqualTo(id.Value));
      Assert.That(@event.Name, Is.EqualTo("Car"));
      Assert.That(@event.CurrentQuantity, Is.EqualTo(1));
      Assert.That(@event.MaximumAllowedQuantity, Is.EqualTo(5));
    }

    [Test]
    public void Ctor_Allows_To_Create_New_Instance_With_Custom_Values_For_Current_Quantity_And_Maximum_Allowed_Quantity()
    {
      // ARRANGE
      var id = InventoryItemId.FromGuid(Guid.NewGuid());
      var name = InventoryItemName.FromString("Car");
      var currentQuantity = Quantity.FromInteger(3);
      var maximumAllowedQuantity = Quantity.FromInteger(7);

      // ACT
      var result = new InventoryItem(id, name, currentQuantity, maximumAllowedQuantity);

      // ASSERT
      Assert.That(result, Is.Not.Null);

      Assert.That(result.Id, Is.EqualTo(id));
      Assert.That(result.Name, Is.EqualTo(name));
      Assert.That(result.IsActive, Is.True);
      Assert.That(result.CurrentQuantity, Is.EqualTo(currentQuantity));
      Assert.That(result.MaximumAllowedQuantity, Is.EqualTo(maximumAllowedQuantity));

      // check events
      var changes = result.GetChanges();

      Assert.That(changes, Is.Not.Null);
      Assert.That(changes, Has.Count.EqualTo(1));
      Assert.That(changes[0], Is.InstanceOf<Events.InventoryItemCreated>());

      var @event = (Events.InventoryItemCreated)changes[0];

      Assert.That(@event.Id, Is.EqualTo(id.Value));
      Assert.That(@event.Name, Is.EqualTo("Car"));
      Assert.That(@event.CurrentQuantity, Is.EqualTo(3));
      Assert.That(@event.MaximumAllowedQuantity, Is.EqualTo(7));
    }

    [Test]
    public void Ctor_Throws_InvalidEntityStateException_When_Current_Quantity_Is_Greater_Than_Maximum_Allowed_Quantity()
    {
      // ARRANGE
      var id = InventoryItemId.FromGuid(Guid.Parse("0ef70438-4a07-4018-8744-f3f71d34bbd4"));
      var name = InventoryItemName.FromString("Car");
      var currentQuantity = Quantity.FromInteger(4);
      var maximumAllowedQuantity = Quantity.FromInteger(2);

      // ACT
      var exception = Assert.Throws<InvalidEntityStateException>(() => new InventoryItem(
        id,
        name,
        currentQuantity,
        maximumAllowedQuantity)
      );

      // ASSERT
      const string expectedMessage =
        "State change for entity of type InventoryItem with Id 0ef70438-4a07-4018-8744-f3f71d34bbd4 has been rejected. "
          +
        "Current quantity must be less than or equal to maximum allowed quantity.";

      Assert.That(
        exception.Message,
        Is.EqualTo(expectedMessage)
      );
    }
  }
}
