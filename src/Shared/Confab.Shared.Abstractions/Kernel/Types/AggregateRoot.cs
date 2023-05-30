﻿namespace Confab.Shared.Abstractions.Kernel.Types
{
    public abstract class AggregateRoot<T>
    {
        private bool _versionIncremented;
        public T Id { get; protected set; }
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;

        private readonly List<IDomainEvent> _events = new();

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any() && !_versionIncremented)
            {
                Version++;
            }

            _versionIncremented = true;
            _events.Add(@event);
        }

        protected void CleaEvents() => _events.Clear();

        protected void IncrementVersion()
        {
            if (!_versionIncremented)
            {
                Version++;
                _versionIncremented = true;
            }
        }
    }

    public abstract class AggregateRoot : AggregateRoot<AggregateId> { }
}
