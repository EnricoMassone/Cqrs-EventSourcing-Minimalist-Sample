namespace Inventory.Domain.Exceptions
{
  public class InvalidEntityStateException : Exception
  {
    public InvalidEntityStateException()
    {
    }

    public InvalidEntityStateException(
      string? message) : base(message)
    {
    }

    public InvalidEntityStateException(
      string? message,
      Exception? innerException) : base(message, innerException)
    {
    }
  }
}
