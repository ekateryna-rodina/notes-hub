using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Dal.Config
{
    public class ReferenceConfig : IEntityTypeConfiguration<Reference>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Reference> builder)
        {
            builder.ToTable("References");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                id => ReferenceId.Create(id)
            );
            builder.Property(t => t.Name)
                .HasMaxLength(100);

            builder
          .HasOne<UserProfile>(n => n.UserProfile)
          .WithMany(up => up.References)
          .HasForeignKey(n => n.UserProfileId)
          .IsRequired()
          .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne<Notebook>(n => n.Notebook)
            .WithMany(up => up.References)
            .HasForeignKey(n => n.NotebookId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
