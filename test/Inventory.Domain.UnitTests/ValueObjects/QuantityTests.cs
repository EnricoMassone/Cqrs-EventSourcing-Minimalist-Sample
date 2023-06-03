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
    public void FromInteger_Creates_New_Instance_Of_Quantity_From_Integer_Value()
    {
      // ACT
      var result = Quantity.FromInteger(13);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(13));
    }
  }
}
