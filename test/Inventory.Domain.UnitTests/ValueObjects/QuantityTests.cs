using AutoFixture.NUnit3;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.ValueObjects
{
  [TestFixture]
  public sealed class QuantityTests
  {
    [Test]
    public void FromInteger_Throws_ArgumentOutOfRangeException_When_Value_Is_Less_Than_Zero()
    {
      // ACT
      var exception = Assert.Throws<ArgumentOutOfRangeException>(
        () => Quantity.FromInteger(-3)
      );

      // ASSERT
      Assert.That(
        exception.ParamName,
        Is.EqualTo("value")
      );

      Assert.That(
        exception.Message,
        Contains.Substring("-3 is not a valid quantity. Quantity must be positive integer number.")
      );
    }

    [Test]
    public void FromInteger_Throws_ArgumentOutOfRangeException_When_Value_Equals_Zero()
    {
      // ACT
      var exception = Assert.Throws<ArgumentOutOfRangeException>(
        () => Quantity.FromInteger(0)
      );

      // ASSERT
      Assert.That(
        exception.ParamName,
        Is.EqualTo("value")
      );

      Assert.That(
        exception.Message,
        Contains.Substring("0 is not a valid quantity. Quantity must be positive integer number.")
      );
    }

    [Test]
    public void FromInteger_Creates_New_Instance_Of_Quantity_From_Positive_Integer_Value()
    {
      // ACT
      var result = Quantity.FromInteger(13);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(13));
    }

    [Test]
    [AutoData]
    public void Should_Be_Possible_To_Implicitly_Convert_From_Quantity_To_Integer()
    {
      // ARRANGE
      var target = Quantity.FromInteger(22);

      // ACT
      int result = target;

      // ASSERT
      Assert.That(result, Is.EqualTo(22));
    }

    [Test]
    [AutoData]
    public void Quantity_Should_Have_Value_Equality()
    {
      // ARRANGE
      var quantity1 = Quantity.FromInteger(22);
      var quantity2 = Quantity.FromInteger(22);

      // ACT
      var result = quantity1.Equals(quantity2);

      // ASSERT
      Assert.That(result, Is.True);
    }
  }
}
