using PersonalBlog.Core.Security;
using PersonalBlog.DataAccess;
using PersonalBlog.DataAccess.DbContext;
using PersonalBlog.Repository.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Repository.EntityFramework.Concrete
{
    public class UserRepository:EfRepository<User>,IUserRepository
    {
        public UserRepository(PersonalBlogDbContext context, IEncryption encryption) :base(context,encryption)
        {

        }
    }
}
