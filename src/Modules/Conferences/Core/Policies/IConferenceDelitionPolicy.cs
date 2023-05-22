using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal interface IConferenceDelitionPolicy
    {
        Task<bool> CanDeleteAsync(Conference conference);
    }
}
