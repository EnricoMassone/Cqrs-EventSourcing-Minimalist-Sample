using System.Collections.ObjectModel;

namespace Inventory.Domain.Framework
{
  public abstract class AggregateRoot<TId>
  {
    private readonly List<object> _changes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected AggregateRoot()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
      this._changes = new List<object>();
      this.Version = -1;
    }

    public TId Id { get; protected set; }
    public int Version { get; private set; }

    protected abstract void ApplyToState(object @event);

    protected abstract void EnsureValidState();

    protected void RaiseEvent(object @event)
    {
      ApplyToState(@event);

      EnsureValidState();

      _changes.Add(@event);
    }

    public void Load(IEnumerable<object> history)
    {
      if (history is null)
      {
        throw new ArgumentNullException(nameof(history));
      }

      foreach (var @event in history)
      {
        ApplyToState(@event);
        Version++;
      }
    }

    public void ClearChanges() => _changes.Clear();

    public ReadOnlyCollection<object> GetChanges() => _changes.AsReadOnly();
  }
}
