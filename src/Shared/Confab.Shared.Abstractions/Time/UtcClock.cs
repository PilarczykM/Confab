namespace Confab.Shared.Abstractions.Time
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}
