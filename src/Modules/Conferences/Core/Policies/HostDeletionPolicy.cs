using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal class HostDeletionPolicy : IHostDeletionPolicy
    {
        private readonly IConferenceDelitionPolicy _conferenceDelitionPolicy;

        public HostDeletionPolicy(IConferenceDelitionPolicy conferenceDelitionPolicy)
        {
            this._conferenceDelitionPolicy = conferenceDelitionPolicy;
        }

        public async Task<bool> CanDeleteAsync(Host host)
        {
            if (host.Conferences is null || !host.Conferences.Any())
            {
                return true;
            }

            foreach (var conference in host.Conferences)
            {
                if (await _conferenceDelitionPolicy.CanDeleteAsync(conference) is false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
