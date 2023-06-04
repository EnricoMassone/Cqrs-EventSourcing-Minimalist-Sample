namespace Inventory.Domain
{
  public static class Events
  {
    public sealed class InventoryItemCreated
    {
      public Guid Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public int CurrentQuantity { get; set; }
      public int MaximumAllowedQuantity { get; set; }
    }

    public sealed class InventoryItemNameChanged
    {
      public Guid Id { get; set; }
      public string NewName { get; set; } = string.Empty;
    }
  }
}
