using System;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class CommentConfig : IEntityTypeConfiguration<Comment>
	{
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(up => up.Id);

            builder
                .HasOne<Note>(nc => nc.Note)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.NoteId)
                .IsRequired();

            builder
               .HasOne<Comment>(nc => nc.Commented)
               .WithMany(n => n.Comments)
               .HasForeignKey(c => c.CommentId)
               .IsRequired(false);

            builder.HasOne<UserProfile>(ci => ci.UserProfile).WithMany(up => up.Comments);
        }
    }
}

