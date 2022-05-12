using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PersonalBlog.DataAccess.DbContext;

public partial class PersonalBlogDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PersonalBlogDbContext()
        {
        }

        public PersonalBlogDbContext(DbContextOptions<PersonalBlogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<PostMeta> PostMetas { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=1234;Server=localhost;Port=5432;Database=BlogDB;Integrated Security=true;Pooling=true;");
            }
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        }  
    }