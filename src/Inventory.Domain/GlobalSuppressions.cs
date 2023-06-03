// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Ignore warning since this is an application. This rule is designed for shared libraries", Scope = "member", Target = "~M:Inventory.Domain.ValueObjects.InventoryItemId.op_Implicit(Inventory.Domain.ValueObjects.InventoryItemId)~System.Guid")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Implicit conversion must not throw exceptions", Scope = "member", Target = "~M:Inventory.Domain.ValueObjects.InventoryItemId.op_Implicit(Inventory.Domain.ValueObjects.InventoryItemId)~System.Guid")]
[assembly: SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Ignore warning since this is an application. This rule is designed for shared libraries", Scope = "member", Target = "~M:Inventory.Domain.ValueObjects.Quantity.op_Implicit(Inventory.Domain.ValueObjects.Quantity)~System.Int32")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Implicit conversion must not throw exceptions", Scope = "member", Target = "~M:Inventory.Domain.ValueObjects.Quantity.op_Implicit(Inventory.Domain.ValueObjects.Quantity)~System.Int32")]
