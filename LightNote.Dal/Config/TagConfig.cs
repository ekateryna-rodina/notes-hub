using System.Reflection.Emit;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
            .ValueGeneratedNever();
            builder.Property(t => t.Id)
                .HasConversion(
                id => id.Value,
                id => TagId.Create(id)
            );
            builder.Property(t => t.Name)
                .HasMaxLength(20);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.UpdatedAt)
                .HasComputedColumnSql("getutcdate()");
        }
    }
}