using LightNote.Domain.Models.NotebookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
    public class NotebookConfig : IEntityTypeConfiguration<Notebook>
    {
        public void Configure(EntityTypeBuilder<Notebook> builder)
        {
            builder.HasKey(up => up.Id);
        }
    }
}

