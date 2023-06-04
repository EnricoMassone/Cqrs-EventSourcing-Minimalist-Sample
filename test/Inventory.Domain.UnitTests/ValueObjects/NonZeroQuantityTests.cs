using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.ValueObjects
{
  [TestFixture]
  public sealed class NonZeroQuantityTests
  {
    [Test]
    public void FromInteger_Throws_ArgumentOutOfRangeException_When_Value_Is_Less_Than_Zero()
    {
      // ACT
      var exception = Assert.Throws<ArgumentOutOfRangeException>(
        () => NonZeroQuantity.FromInteger(-3)
      );

      // ASSERT
      Assert.That(
        exception.ParamName,
        Is.EqualTo("value")
      );

      Assert.That(
        exception.Message,
        Contains.Substring("-3 is not a valid quantity. Quantity must be non negative integer number.")
      );
    }

    [Test]
    public void FromInteger_Throws_ArgumentOutOfRangeException_When_Value_Equals_Zero()
    {
      // ACT
      var exception = Assert.Throws<ArgumentOutOfRangeException>(
        () => NonZeroQuantity.FromInteger(0)
      );

      // ASSERT
      Assert.That(
        exception.ParamName,
        Is.EqualTo("value")
      );

      Assert.That(
        exception.Message,
        Contains.Substring("Value of 0 is invalid for an instance of NonZeroQuantity. Value must be positive integer number.")
      );
    }

    [Test]
    public void FromInteger_Creates_New_Instance_Of_NonZeroQuantity_From_Positive_Integer_Value()
    {
      // ACT
      var result = NonZeroQuantity.FromInteger(13);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(13));
    }
  }
}
