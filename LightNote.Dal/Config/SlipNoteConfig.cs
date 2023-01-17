using System;
using System.Reflection.Emit;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.Domain.Models.UserProfileAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class SlipNoteConfig : IEntityTypeConfiguration<SlipNote>
    {
        public void Configure(EntityTypeBuilder<SlipNote> builder)
        {
            builder.ToTable("SlipNotes");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(p => p.Value, p => SlipNoteId.Create(p));
            builder.Property(t => t.Content).HasConversion(p => p.Value, p => Content.Create(p));
            builder.Property(t => t.Content).HasMaxLength(150);
            builder.Property(t => t.UserProfileId).HasConversion(p => p.Value, p => UserProfileId.Create(p));
            builder.Property(t => t.NotebookId).HasConversion(p => p.Value, p => NotebookId.Create(p));

            builder
              .HasOne<Notebook>(n => n.Notebook)
              .WithMany(up => up.SlipNotes)
              .HasForeignKey(n => n.NotebookId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder
             .HasOne<UserProfile>(n => n.UserProfile)
             .WithMany(up => up.SlipNotes)
             .HasForeignKey(n => n.UserProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne<Reference>(t => t.Reference)
                .WithMany(t => t.SlipNotes)
                .HasForeignKey(t => t.ReferenceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

