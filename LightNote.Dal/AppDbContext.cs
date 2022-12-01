
using System;
using LightNote.Domain.Models.Note;
using LightNote.Domain.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Dal
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}

