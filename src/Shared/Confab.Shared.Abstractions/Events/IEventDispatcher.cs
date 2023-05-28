namespace Confab.Shared.Abstractions.Events
{
    public interface IEventDispatcher
    {
        Task PublicAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}

