using AutoMapper;
using PersonalBlog.Business.Abstract;
using PersonalBlog.DataAccess;
using PersonalBlog.Dto.Dto;
using PersonalBlog.Repository.EntityFramework.Abstract;

namespace PersonalBlog.Business.Concrete
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper= mapper;
        }

        public Task<bool> Register(RegisterDto userRegisterDto)
        {
            User registerUser = _mapper.Map<User>(userRegisterDto);
            return _userRepository.InsertAsync(registerUser);
        }
    }
}
