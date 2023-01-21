using System;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
    public class ReferenceTagConfig : IEntityTypeConfiguration<ReferenceTag>
    {
        public void Configure(EntityTypeBuilder<ReferenceTag> builder)
        {
            builder.ToTable("ReferenceTags");
            builder.HasKey(t => new { t.ReferenceId, t.TagId });
            builder.Property(t => t.ReferenceId).HasConversion(t => t.Value, t => ReferenceId.Create(t));
            builder.Property(t => t.TagId).HasConversion(t => t.Value, t => TagId.Create(t));
            builder
                .HasOne(t => t.Reference)
                .WithMany(t => t.TagsAttached)
                .HasForeignKey(t => t.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(t => t.Tag)
                .WithMany(t => t.ReferencesAttached)
                .HasForeignKey(t => t.TagId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

