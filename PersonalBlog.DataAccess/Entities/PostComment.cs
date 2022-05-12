using PersonalBlog.Core;

namespace PersonalBlog.DataAccess;

public class Comment:BaseEntity
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid ParentId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }

    public virtual Comment ParentComment { get; set; }
    public virtual ICollection<Comment> SubComments { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public virtual Post Post { get; set; }
}