namespace Inventory.Domain.Exceptions
{
  public class InvalidInventoryIdException : Exception
  {
    public InvalidInventoryIdException()
    {
    }

    public InvalidInventoryIdException(
      string message) : base(message)
    {
    }

    public InvalidInventoryIdException(
      string message,
      Exception innerException) : base(message, innerException)
    {
    }
  }
}
