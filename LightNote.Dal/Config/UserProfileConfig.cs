using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
    public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(up => up.Id);
            builder.Property(p => p.Id).ValueGeneratedNever().HasConversion(p => p.Value, p => UserProfileId.Create(p));
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.UpdatedAt)
                .HasComputedColumnSql("GETUTCDATE()");
        }
    }
}

