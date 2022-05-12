using PersonalBlog.Core;

namespace PersonalBlog.DataAccess;

public class Tag:BaseEntity
{
    public Tag()
    {
        Posts = new HashSet<Post>();
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string MetaTitle { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; }
}