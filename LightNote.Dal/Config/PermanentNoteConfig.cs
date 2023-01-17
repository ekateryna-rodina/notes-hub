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
    public class PermanentNoteConfig : IEntityTypeConfiguration<PermanentNote>
    {
        public void Configure(EntityTypeBuilder<PermanentNote> builder)
        {
            builder.ToTable("PermanentNotes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(p => p.Value, p => PermanentNoteId.Create(p));
            builder.Property(p => p.Title).HasMaxLength(50).HasConversion(p => p.Value, p => Title.Create(p));
            builder.Property(p => p.Content).HasMaxLength(150).HasConversion(p => p.Value, p => Content.Create(p));
            builder.Property(p => p.UserProfileId).HasConversion(p => p.Value, p => UserProfileId.Create(p));
            builder.Property(p => p.NotebookId).HasConversion(p => p.Value, p => NotebookId.Create(p));

            builder
            .HasMany(p => p.SlipNotes)
            .WithOne(p => p.PermanentNote)
            .HasForeignKey(sn => sn.PermanentNoteId)
            .IsRequired(false);


            builder
            .HasOne<Notebook>(n => n.Notebook)
            .WithMany(up => up.PermanentNotes)
            .HasForeignKey(n => n.NotebookId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            builder
             .HasOne<UserProfile>(n => n.UserProfile)
             .WithMany(up => up.PermanentNotes)
             .HasForeignKey(n => n.UserProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

            builder
              .HasMany(i => i.Questions)
              .WithOne(q => q.PermanentNote)
              .HasForeignKey(q => q.PermanentNoteId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

