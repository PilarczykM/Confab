namespace Confab.Shared.Infrastructure.Modules
{
    public sealed class ModuleBroadcastRegistration
    {
        public Type ReciverType { get; }
        public Func<object, Task> Action { get; }
        public string Key => ReciverType.Name;

        public ModuleBroadcastRegistration(Func<object, Task> action, Type reciverType)
        {
            Action = action;
            ReciverType = reciverType;
        }
    }
}
