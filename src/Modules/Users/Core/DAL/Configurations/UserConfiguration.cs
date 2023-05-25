using System.Text.Json;
using Confab.Modules.Users.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Users.Core.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.Claims)
                .HasConversion(x => JsonSerializer.Serialize(x, jsonSerializerOptions),
                    x => JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(x, jsonSerializerOptions));

            builder.Property(x => x.Claims).Metadata.SetValueComparer(
                new ValueComparer<Dictionary<string, IEnumerable<string>>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToDictionary(x => x.Key, x => x.Value)));
        }
    }
}

