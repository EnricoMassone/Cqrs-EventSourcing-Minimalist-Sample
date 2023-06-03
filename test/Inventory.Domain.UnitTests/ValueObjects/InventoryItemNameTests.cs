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

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Starts_With_White_Space()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString(" invalid")
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("' invalid' is not a valid inventory item name")
      );
    }

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Starts_With_Number()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString("5invalid")
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("5invalid is not a valid inventory item name")
      );
    }

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Contains_Underscore()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString("hello_world")
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("hello_world is not a valid inventory item name")
      );
    }

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Contains_Hyphen()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString("hello-world")
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("hello-world is not a valid inventory item name")
      );
    }

    [TestCase("Invalid!")]
    [TestCase("Invalid?")]
    [TestCase("In$valid%")]
    [TestCase("P@sswor$")]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Contains_Special_Characters(string value)
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString(value)
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo($"{value} is not a valid inventory item name")
      );
    }

    [Test]
    public void FromString_Throws_InvalidInventoryItemNameException_When_Value_Is_Longer_Than_100_Characters()
    {
      // ARRANGE
      var value = new string('a', 101);

      // ACT
      var exception = Assert.Throws<InvalidInventoryItemNameException>(
        () => InventoryItemName.FromString(value)
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo($"{value} is not a valid inventory item name")
      );
    }

    [TestCase("car")]
    [TestCase("CAR")]
    [TestCase("Car")]
    [TestCase("Green Car")]
    [TestCase("Car 123")]
    public void FromString_Creates_New_Instance_Of_InventoryItemName_With_Provided_String_Value(string value)
    {
      // ACT
      var result = InventoryItemName.FromString(value);

      // ASSERT
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Value, Is.EqualTo(value));
    }
  }
}
