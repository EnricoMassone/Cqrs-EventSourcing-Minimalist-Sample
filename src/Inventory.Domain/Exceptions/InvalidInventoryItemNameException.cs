namespace Inventory.Domain.Exceptions
{
  public class InvalidInventoryItemNameException : Exception
  {
    public InvalidInventoryItemNameException()
    {
    }

    public InvalidInventoryItemNameException(
      string? message) : base(message)
    {
    }

    public InvalidInventoryItemNameException(
      string? message,
      Exception? innerException) : base(message, innerException)
    {
    }
  }
}
