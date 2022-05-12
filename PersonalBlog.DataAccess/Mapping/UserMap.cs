using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.DataAccess.Mapping
{
    class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.FirstName).IsRequired();

            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired();

            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.PasswordHash).HasMaxLength(250);
            builder.Property(x => x.PasswordHash).IsRequired();

            builder.Property(x => x.RegisteredAt).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(x => x.Intro).HasMaxLength(250);
            builder.Property(x => x.Profile).HasMaxLength(250);

            builder.ToTable("Users");

            builder.HasData(
                new User
                {
                    Id = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                    RegisteredAt = DateTime.Now.ToUniversalTime(),
                    Profile = "omerfrkgrsy",
                    LastName = "Gürsoy",
                    FirstName = "Ömer",
                    Email = "ömer@ömer.com",
                    PasswordHash = "$2a$11$uNx/XA0odP6BAp8xKqtkausOYVPqmGNmq1GYK/y0E6OgQNb/7XIfC",
                    Intro = "Bu profil Ömerin Özel"
                }
            );
        }
    }
}
