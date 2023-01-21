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
    public class InsightConfig : IEntityTypeConfiguration<Insight>
    {

        public void Configure(EntityTypeBuilder<Insight> builder)
        {
            builder.ToTable("Insights");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    id => InsightId.Create(id)
            );
            builder.Property(t => t.Content)
                .HasMaxLength(150);
            builder.Property(t => t.UserProfileId)
                .HasConversion(
                    id => id.Value,
                    id => UserProfileId.Create(id)
                );
            builder
                .Property(t => t.Content)
                .HasConversion(
                c => c.Value,
                c => Content.Create(c));
            builder
              .Property(t => t.Title)
              .HasConversion(
              c => c.Value,
              c => Title.Create(c));
            builder
                .Property(t => t.NotebookId)
                .HasConversion(
                    id => id.Value,
                    id => NotebookId.Create(id)
                );
            builder
                 .HasOne<Notebook>(n => n.Notebook)
                 .WithMany(up => up.Insights)
                 .HasForeignKey(n => n.NotebookId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder
                 .HasOne<UserProfile>(n => n.UserProfile)
                 .WithMany(up => up.Insights)
                 .HasForeignKey(n => n.UserProfileId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasMany(i => i.Questions)
               .WithOne(q => q.Insight)
               .HasForeignKey(q => q.InsightId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

