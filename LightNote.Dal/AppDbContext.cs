using System;
using System.Reflection;
using LightNote.Dal.Config;
using LightNote.Domain.Models.Common.BaseModels;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.UserProfileAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Dal
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; } = default!;
        public DbSet<Notebook> Notebooks { get; set; } = default!;
        public DbSet<Tag> Tags { get; set; } = default!;
        public DbSet<Reference> References { get; set; } = default!;
        public DbSet<SlipNote> SlipNotes { get; set; } = default!;
        public DbSet<PermanentNote> PermanentNotes { get; set; } = default!;
        public DbSet<Insight> Insights { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<InsightPermanentNote> InsightPermanentNote { get; set; } = default!;
        public DbSet<ReferenceTag> ReferenceTags { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetValueObjectsAsNonGenerated(modelBuilder);
            ApplyAllConfigurations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
            modelBuilder.ApplyConfiguration(new NotebookConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new ReferenceConfig());
            modelBuilder.ApplyConfiguration(new SlipNoteConfig());
            modelBuilder.ApplyConfiguration(new PermanentNoteConfig());
            modelBuilder.ApplyConfiguration(new InsightConfig());
            modelBuilder.ApplyConfiguration(new QuestionConfig());
            // Configuration for many-to-many
            modelBuilder.ApplyConfiguration(new InsightPermanentNoteConfig());
            modelBuilder.ApplyConfiguration(new ReferenceTagConfig());
            modelBuilder.ApplyConfiguration(new QuestionReferenceConfig());

        }

        private void SetValueObjectsAsNonGenerated(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ValueObject).Assembly;
            var valueTypes = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ValueObject)));
            modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                .Where(p => valueTypes.Contains(p.ClrType))
                .ToList().ForEach(p => p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never);
        }
    }
}

