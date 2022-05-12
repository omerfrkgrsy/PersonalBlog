using PersonalBlog.Core;

namespace PersonalBlog.DataAccess;

public class User:BaseEntity
{
    public User()
    {
        Posts = new HashSet<Post>();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public string Intro { get; set; }
    public string Profile { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; }
}