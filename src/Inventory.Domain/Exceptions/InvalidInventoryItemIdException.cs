namespace Inventory.Domain.Exceptions
{
  public class InvalidInventoryItemIdException : Exception
  {
    public InvalidInventoryItemIdException()
    {
    }

    public InvalidInventoryItemIdException(
      string message) : base(message)
    {
    }

    public InvalidInventoryItemIdException(
      string message,
      Exception innerException) : base(message, innerException)
    {
    }
  }
}
