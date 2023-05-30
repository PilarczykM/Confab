using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public sealed class Speaker : AggregateRoot
    {
        private readonly ICollection<Submission> _submission;

        public IEnumerable<Submission> Submissions => _submission;

        public string FullName { get; init; }

        private Speaker() { }

        public Speaker(AggregateId id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public static Speaker Create(Guid id, string fullName) => new(id, fullName);
    }
}
