using Inventory.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Inventory.Domain.ValueObjects
{
  public sealed record InventoryItemName
  {
    private static readonly Regex s_validationRegex = new(
      "^[a-zA-Z]{1}[a-zA-Z 0-9]{0,99}$",
      RegexOptions.Compiled
    );

    public static InventoryItemName FromString(string value)
    {
      if (value is null)
      {
        throw new ArgumentNullException(nameof(value));
      }

      if (!IsValidName(value))
      {
        throw NewInvalidInventoryItemNameException(value);
      }

      return new InventoryItemName(value);

      static bool IsValidName(string value) =>
        s_validationRegex.IsMatch(value);

      static InvalidInventoryItemNameException NewInvalidInventoryItemNameException(string value)
      {
        var printedValue = string.IsNullOrWhiteSpace(value) || value.StartsWith(' ') ?
          $"'{value}'" :
          value;

        var message = $"{printedValue} is not a valid inventory item name";

        return new InvalidInventoryItemNameException(message);
      }
    }

    public string Value { get; }

    internal InventoryItemName(string value) => this.Value = value;

    public static implicit operator string(InventoryItemName inventoryItemName) =>
      inventoryItemName.Value;
  }
}
