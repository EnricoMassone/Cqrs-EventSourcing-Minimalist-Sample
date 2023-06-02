using AutoFixture.NUnit3;
using Inventory.Domain.Exceptions;
using Inventory.Domain.ValueObjects;

namespace Inventory.Domain.UnitTests.ValueObjects
{
  [TestFixture]
  public sealed class InventoryIdTests
  {
    [Test]
    public void FromGuid_Throws_InvalidInventoryIdException_When_Value_Is_Empty_Guid()
    {
      // ACT
      var exception = Assert.Throws<InvalidInventoryIdException>(
        () => InventoryId.FromGuid(Guid.Empty)
      );

      // ASSERT
      Assert.That(
        exception.Message,
        Is.EqualTo("Value 00000000-0000-0000-0000-000000000000 is not valid for Inventory Id")
      );
    }

    [Test]
    [AutoData]
    public void FromGuid_Creates_New_Instance_Of_InventoryId_From_Guid_Value(Guid value)
    {
      // ACT
      var result = InventoryId.FromGuid(value);

      // ASSERT
      Assert.That(result.Value, Is.EqualTo(value));
    }
  }
}
