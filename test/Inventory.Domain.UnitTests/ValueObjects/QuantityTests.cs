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
        Contains.Substring("-3 is not a valid quantity. Quantity must be non negative integer number.")
      );
    }

    [TestCase(13)]
    [TestCase(0)]
    public void FromInteger_Creates_New_Instance_Of_Quantity_From_Non_Negative_Integer_Value(int value)
    {
      // ACT
      var result = Quantity.FromInteger(value);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(value));
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
    public void Add_Throws_ArgumentNullException_When_Other_Is_Null()
    {
      // ARRANGE
      var target = Quantity.FromInteger(4);

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => target.Add(null!)
      );

      // ASSERT
      Assert.That(exception.ParamName, Is.EqualTo("other"));
    }

    [Test]
    public void Add_Creates_New_Instance_Of_Quantity_By_Summing_Values_Of_Current_Instance_And_Other()
    {
      // ARRANGE
      var target = Quantity.FromInteger(4);
      var other = Quantity.FromInteger(12);

      // ACT
      var result = target.Add(other);

      // ASSERT
      Assert.That(result, Is.Not.Null);
      Assert.That(result, Is.Not.SameAs(target));
      Assert.That(result.Value, Is.EqualTo(16));
    }

    [Test]
    public void Subtract_Throws_ArgumentNullException_When_Other_Is_Null()
    {
      // ARRANGE
      var target = Quantity.FromInteger(4);

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => target.Subtract(null!)
      );

      // ASSERT
      Assert.That(exception.ParamName, Is.EqualTo("other"));
    }

    [TestCase(13, 5, 8)]
    [TestCase(13, 13, 0)]
    public void Subtract_Creates_New_Instance_Of_Quantity_By_Subtracting_Values_Of_Current_Instance_And_Other(
      int currentValue,
      int otherValue,
      int resultValue)
    {
      // ARRANGE
      var target = Quantity.FromInteger(currentValue);
      var other = Quantity.FromInteger(otherValue);

      // ACT
      var result = target.Subtract(other);

      // ASSERT
      Assert.That(result, Is.Not.Null);
      Assert.That(result, Is.Not.SameAs(target));
      Assert.That(result.Value, Is.EqualTo(resultValue));
    }

    [Test]
    public void Subtract_Throws_InvalidOperationException_When_Current_Instance_Value_Is_Less_Than_Other_Instance_Value()
    {
      // ARRANGE
      var target = Quantity.FromInteger(10);
      var other = Quantity.FromInteger(20);

      // ACT
      var exception = Assert.Throws<InvalidOperationException>(
        () => target.Subtract(other)
      );

      // ASSERT
      Assert.That(exception.Message, Is.EqualTo("Cannot subtract quantity of 20 from quantity of 10"));
    }

    #region IComparable<T> tests

    /*
     * See https://learn.microsoft.com/en-us/dotnet/api/system.icomparable-1.compareto?view=net-7.0#notes-to-implementers 
     */

    [Test]
    public void Comparison_Between_Quantity_Instances_Should_Be_Based_On_Value_Property()
    {
      // ACT
      var result1 = Quantity.FromInteger(11).CompareTo(Quantity.FromInteger(11));
      var result2 = Quantity.FromInteger(13).CompareTo(Quantity.FromInteger(5));
      var result3 = Quantity.FromInteger(2).CompareTo(Quantity.FromInteger(46));

      // ASSERT
      Assert.That(result1, Is.EqualTo(0));
      Assert.That(result2, Is.GreaterThan(0));
      Assert.That(result3, Is.LessThan(0));
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

    #endregion

    #region Operator overloads tests

    [Test]
    public void GreaterThan_Operator_Compares_Instances_By_Value()
    {
      // ACT
      var result1 = Quantity.FromInteger(11) > Quantity.FromInteger(11);
      var result2 = Quantity.FromInteger(13) > Quantity.FromInteger(5);
      var result3 = Quantity.FromInteger(2) > Quantity.FromInteger(46);

      // ASSERT
      Assert.That(result1, Is.False);
      Assert.That(result2, Is.True);
      Assert.That(result3, Is.False);
    }

    [Test]
    public void LessThan_Operator_Compares_Instances_By_Value()
    {
      // ACT
      var result1 = Quantity.FromInteger(11) < Quantity.FromInteger(11);
      var result2 = Quantity.FromInteger(13) < Quantity.FromInteger(5);
      var result3 = Quantity.FromInteger(2) < Quantity.FromInteger(46);

      // ASSERT
      Assert.That(result1, Is.False);
      Assert.That(result2, Is.False);
      Assert.That(result3, Is.True);
    }

    [Test]
    public void GreaterThanOrEqualTo_Operator_Compares_Instances_By_Value()
    {
      // ACT
      var result1 = Quantity.FromInteger(11) >= Quantity.FromInteger(11);
      var result2 = Quantity.FromInteger(13) >= Quantity.FromInteger(5);
      var result3 = Quantity.FromInteger(2) >= Quantity.FromInteger(46);

      // ASSERT
      Assert.That(result1, Is.True);
      Assert.That(result2, Is.True);
      Assert.That(result3, Is.False);
    }

    [Test]
    public void LessThanOrEqualTo_Operator_Compares_Instances_By_Value()
    {
      // ACT
      var result1 = Quantity.FromInteger(11) <= Quantity.FromInteger(11);
      var result2 = Quantity.FromInteger(13) <= Quantity.FromInteger(5);
      var result3 = Quantity.FromInteger(2) <= Quantity.FromInteger(46);

      // ASSERT
      Assert.That(result1, Is.True);
      Assert.That(result2, Is.False);
      Assert.That(result3, Is.True);
    }

    [Test]
    public void Addition_Operator_Performs_Sum_Of_Provided_Instances()
    {
      // ACT
      var result = Quantity.FromInteger(13) + Quantity.FromInteger(9);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(22));
    }

    [Test]
    public void Subtraction_Operator_Performs_Subtraction_Of_Provided_Instances()
    {
      // ACT
      var result = Quantity.FromInteger(13) - Quantity.FromInteger(9);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(4));
    }

    #endregion
  }
}
