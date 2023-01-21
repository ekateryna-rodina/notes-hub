using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
    public class NotebookConfig : IEntityTypeConfiguration<Notebook>
    {
        public void Configure(EntityTypeBuilder<Notebook> builder)
        {
            builder.ToTable("Notebooks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                id => NotebookId.Create(id)
            );
            builder.Property(p => p.Title)
                .HasConversion(p => p.Value, p => Title.Create(p));
            builder.Property(t => t.Title)
                .HasMaxLength(50);
            builder.Property(t => t.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    id => UserProfileId.Create(id)
                );
            builder.HasOne<UserProfile>(t => t.UserProfile)
                .WithMany(t => t.Notebooks)
                .HasForeignKey(t => t.UserProfileId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.UpdatedAt)
                .HasComputedColumnSql("getutcdate()");
        }
    }
}

