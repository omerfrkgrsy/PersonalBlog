using PersonalBlog.Core;

namespace PersonalBlog.DataAccess;

public class Category:BaseEntity
{
    public Category()
    {
        Posts = new HashSet<Post>();
    }
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public string Title { get; set; }
    public string MetaTitle { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; }
    public virtual Category ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; }
}