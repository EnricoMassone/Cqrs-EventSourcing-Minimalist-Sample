using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.ValueObjects
{
  [TestFixture]
  public sealed class InventoryItemNameTests
  {
    [Test]
    public void FromString_Throws_ArgumentNullException_When_Value_Is_Null()
    {
      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => InventoryItemName.FromString(null!)
      );

      // ASSERT
      Assert.That(
        exception.ParamName,
        Is.EqualTo("value")
      );
    }

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Is_Empty_String()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString(string.Empty)
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("'' is not a valid inventory item name")
      );
    }

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Is_White_Space_String()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString("   ")
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("'   ' is not a valid inventory item name")
      );
    }
  }
}
