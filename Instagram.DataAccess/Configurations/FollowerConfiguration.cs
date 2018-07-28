using System;
using System.Collections.Generic;
using System.Text;
using Instagram.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Instagram.DataAccess.Configurations
{
    public class FollowerConfiguration: IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasOne(pt => pt.FollowingUser)
                .WithMany(t => t.Followers);

            builder.HasOne(pt => pt.FollowerUser)
                .WithMany(t => t.Followings);
        }
    }
}
