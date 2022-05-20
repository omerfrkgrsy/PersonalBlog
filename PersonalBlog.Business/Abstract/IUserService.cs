using PersonalBlog.DataAccess;
using PersonalBlog.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Business.Abstract
{
    public interface IUserService
    {
        Task<bool> Register(RegisterDto userRegisterDto);

    }
}
