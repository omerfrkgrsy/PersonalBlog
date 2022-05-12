using PersonalBlog.Core;

namespace PersonalBlog.DataAccess;

public class PostMeta:BaseEntity
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string Key { get; set; }
    public string Content { get; set; }
    
    public virtual Post Post { get; set; }
}