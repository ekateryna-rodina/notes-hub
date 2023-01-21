using System;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(t => t.Value, t =>  QuestionId.Create(t));
            builder.Property(t => t.Content).HasMaxLength(150)
                .HasConversion(t => t.Value, t => Content.Create(t));
            builder.HasOne(t => t.UserProfile)
                .WithMany(t => t.Questions)
                .HasForeignKey(t => t.UserProfileId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Notebook)
               .WithMany(t => t.Questions)
               .HasForeignKey(t => t.NotebookId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

