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

    [Test]
    public void Quantity_Instance_Should_Be_Greater_Than_Null_Reference()
    {
      // ARRANGE
      var target = Quantity.FromInteger(13);

      // ACT
      var result = target.CompareTo(null);

      // ASSERT
      Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    public void Comparison_Between_Quantity_Instance_And_Itself_Returns_Zero()
    {
      // ARRANGE
      var target = Quantity.FromInteger(13);

      // ACT
      var result = target.CompareTo(target);

      // ASSERT
      Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void When_Comparison_Between_Instances_Returns_Zero_Then_Symmetric_Property_Holds()
    {
      // ARRANGE
      var quantity1 = Quantity.FromInteger(13);
      var quantity2 = Quantity.FromInteger(13);

      // ACT
      var result1 = quantity1.CompareTo(quantity2);
      var result2 = quantity2.CompareTo(quantity1);

      // ASSERT
      Assert.That(result1, Is.EqualTo(0));
      Assert.That(result2, Is.EqualTo(0));
    }

    [Test]
    public void When_Comparison_Between_Instances_Returns_Zero_Then_Transitive_Property_Holds()
    {
      // ARRANGE
      var quantity1 = Quantity.FromInteger(13);
      var quantity2 = Quantity.FromInteger(13);
      var quantity3 = Quantity.FromInteger(13);

      // ACT
      var result1 = quantity1.CompareTo(quantity2);
      var result2 = quantity2.CompareTo(quantity3);
      var result3 = quantity1.CompareTo(quantity3);

      // ASSERT
      Assert.That(result1, Is.EqualTo(0));
      Assert.That(result2, Is.EqualTo(0));
      Assert.That(result3, Is.EqualTo(0));
    }

    [TestCase(11, 45)]
    [TestCase(16, 3)]
    public void When_Comparison_Between_Two_Instances_Returns_Value_Other_Than_Zero_Then_Asymmetric_Property_Holds(int a, int b)
    {
      // ARRANGE
      var quantity1 = Quantity.FromInteger(a);
      var quantity2 = Quantity.FromInteger(b);

      // ACT
      var result1 = quantity1.CompareTo(quantity2);
      var result2 = quantity2.CompareTo(quantity1);

      // ASSERT
      Assert.That(result1 * result2, Is.LessThan(0));
    }

    [TestCase(11, 45, 70)]
    [TestCase(100, 50, 20)]
    public void When_Comparison_Between_Instances_Returns_Value_Other_Than_Zero_Having_Same_Sign_Then_Transitive_Property_Holds(int a, int b, int c)
    {
      // ARRANGE
      var quantity1 = Quantity.FromInteger(a);
      var quantity2 = Quantity.FromInteger(b);
      var quantity3 = Quantity.FromInteger(c);

      // ACT
      var result1 = quantity1.CompareTo(quantity2);
      var result2 = quantity2.CompareTo(quantity3);
      var result3 = quantity1.CompareTo(quantity3);

      // ASSERT
      Assert.That(result1 * result2, Is.GreaterThan(0));
      Assert.That(result1 * result3, Is.GreaterThan(0));
      Assert.That(result2 * result3, Is.GreaterThan(0));
    }
  }
}
