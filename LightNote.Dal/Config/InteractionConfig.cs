using System;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class InteractionConfig : IEntityTypeConfiguration<Interaction>
	{
        public void Configure(EntityTypeBuilder<Interaction> builder)
        {
            builder.HasKey(up => up.Id);
            builder
                .HasOne<Note>(i => i.Note)
                .WithMany(n => n.Interactions)
                .HasForeignKey(i => i.NoteId)
                .IsRequired();

            builder
               .HasOne<Comment>(i => i.Comment)
               .WithMany(n => n.Interactions)
               .HasForeignKey(i => i.CommentId)
               .IsRequired(false);
        }
    }
}

