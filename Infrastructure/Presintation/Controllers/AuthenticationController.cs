
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.IdentityDtos;

namespace Presintation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):APIBaseController
    {
        #region Register
        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }
        #endregion
        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var User=await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }
        #endregion
    }
}
