using System;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class NoteConfig : IEntityTypeConfiguration<Note>
	{
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);

            builder
                .HasOne<UserProfile>(n => n.UserProfile)  
                .WithMany(up => up.Notes)
                .HasForeignKey(n => n.UserId)
                .IsRequired();

            builder
           .HasOne<Reference>(n => n.Reference)
           .WithMany(up => up.Notes)
           .IsRequired();

            builder
                .HasMany<Tag>(n => n.Tags)
                .WithMany(t => t.Notes);

            builder
                .HasMany<Note>(n => n.Links)
                .WithMany();
        }
    }
}

