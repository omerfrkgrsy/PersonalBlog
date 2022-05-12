using PersonalBlog.Core;

namespace PersonalBlog.DataAccess;

public class Post:BaseEntity
{
    public Post()
    {
        Comments = new HashSet<Comment>();
        Categories = new HashSet<Category>();
        Tags = new HashSet<Tag>();
        PostMetas = new HashSet<PostMeta>();
    }
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ParentId { get; set; }
    public string Title { get; set; }
    public string MetaTitle { get; set; }
    public string Slug { get; set; }
    public string Summary { get; set; }
    public bool Published { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string Content { get; set; }
    
    public virtual User User { get; set; }
    public virtual Post ParentPost { get; set; }
    public virtual ICollection<Post> SubPosts { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
    public virtual ICollection<PostMeta> PostMetas { get; set; }


}