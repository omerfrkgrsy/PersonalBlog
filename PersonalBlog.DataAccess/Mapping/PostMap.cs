using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.DataAccess.Mapping
{
    internal class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(x => x.Title).IsRequired().HasMaxLength(60);
            
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(80);

            builder.Property(x => x.Summary).IsRequired().HasMaxLength(500);

            builder.Property(x => x.Content).IsRequired().HasMaxLength(2000);

            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());



            //Relationship
            builder.HasOne(x => x.User).WithMany(y => y.Posts).HasForeignKey(z => z.AuthorId);
            builder.HasMany(x => x.SubPosts).WithOne(y => y.ParentPost).HasForeignKey(z => z.ParentId);
            builder.HasMany(x => x.Categories).WithMany(y => y.Posts).UsingEntity(x=> x.ToTable("PostCategories"));
            builder.HasMany(x => x.Comments).WithMany(y => y.Posts).UsingEntity(x=> x.ToTable("PostComments"));
            builder.HasMany(x => x.Tags).WithMany(y => y.Posts).UsingEntity(x=> x.ToTable("PostTags"));


            builder.ToTable("Posts");
        }
    }
}
