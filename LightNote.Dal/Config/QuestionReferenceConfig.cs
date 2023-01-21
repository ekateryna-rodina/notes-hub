using System;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class QuestionReferenceConfig : IEntityTypeConfiguration<QuestionReference>
    {
        public void Configure(EntityTypeBuilder<QuestionReference> builder)
        {
            builder.ToTable("QuestionReferences");
            builder.HasKey(t => new { t.QuestionId, t.ReferenceId });
            builder.Property(t => t.ReferenceId).HasConversion(t => t.Value, t => ReferenceId.Create(t));
            builder.Property(t => t.QuestionId).HasConversion(t => t.Value, t => QuestionId.Create(t));
            builder
                .HasOne(t => t.Reference)
                .WithMany(t => t.RelatedQuestions)
                .HasForeignKey(t => t.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(t => t.Question)
                .WithMany(t => t.ReferencesFound)
                .HasForeignKey(t => t.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
