﻿using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public sealed class Speaker : AggregateRoot
    {
        public string FullName { get; init; }

        public Speaker(AggregateId id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public static Speaker Create(Guid id, string fullName) => new(id, fullName);
    }
}
