namespace Confab.Shared.Infrastructure.Modules
{
    public sealed class ModuleRequestRegistration
    {
        public Type ResponseType { get; }
        public Type RequestType { get; }
        public Func<object, Task<object>> Action { get; }

        public ModuleRequestRegistration(
            Type requestType,
            Type responseType,
            Func<object, Task<object>> action
        )
        {
            ResponseType = responseType;
            RequestType = requestType;
            Action = action;
        }
    }
}
