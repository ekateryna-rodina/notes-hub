using System;
using LightNote.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LightNote.Dal.Config
{
	public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
	{
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up => up.Id);
            builder.OwnsOne<BasicUserInfo>(up => up.BasicUserInfo);
            builder.OwnsOne<Subscription>(up => up.Subscription);
        }
    }
}

