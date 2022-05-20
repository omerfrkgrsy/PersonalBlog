using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Abstract;
using PersonalBlog.Dto.Dto;

namespace PersonalBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto loginDto)
        {
            var res = await _userService.Register(loginDto);
            if (!res)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

    }
}