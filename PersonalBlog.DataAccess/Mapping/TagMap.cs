using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.DataAccess.Mapping
{
    internal class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Content).IsRequired().HasMaxLength(300);

            //Relationship

            builder.ToTable("PostComments");
        }
    }
}
