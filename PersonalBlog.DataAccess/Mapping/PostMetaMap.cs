using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.DataAccess.Mapping
{
    internal class PostMetaMap : IEntityTypeConfiguration<PostMeta>
    {
        public void Configure(EntityTypeBuilder<PostMeta> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(x => x.Key).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Content).IsRequired().HasMaxLength(300);

            //Relationship
            builder.HasOne(x => x.Post).WithMany(y => y.PostMetas).HasForeignKey(z => z.PostId);

            builder.ToTable("PostComments");
        }
    }
}
