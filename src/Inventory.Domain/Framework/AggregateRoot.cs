using System.Collections.ObjectModel;

namespace Inventory.Domain.Framework
{
  public abstract class AggregateRoot<TId>
  {
    private readonly List<object> _changes;

    protected AggregateRoot()
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
