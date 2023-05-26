using Confab.Shared.Abstractions.Context;

namespace Confab.Shared.Infrastructure.Context
{
    internal interface IContextFactory
    {
        IContext Create();
    }
}
