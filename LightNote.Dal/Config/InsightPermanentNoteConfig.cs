using System;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class InsightPermanentNoteConfig : IEntityTypeConfiguration<InsightPermanentNote>
	{
        public void Configure(EntityTypeBuilder<InsightPermanentNote> builder)
        {
            builder.ToTable("InsightPermanentNotes");
            builder.HasKey(t => new {t.InsightId, t.PermanentNoteId });
            builder.Property(t => t.InsightId).HasConversion(t => t.Value, t => InsightId.Create(t));
            builder.Property(t => t.PermanentNoteId).HasConversion(t => t.Value, t => PermanentNoteId.Create(t));
            builder
                .HasOne(t => t.Insight)
                .WithMany(t => t.BasedOnPermanentNotes)
                .HasForeignKey(t => t.InsightId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(t => t.PermanentNote)
                .WithMany(t => t.Insights)
                .HasForeignKey(t => t.PermanentNoteId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

