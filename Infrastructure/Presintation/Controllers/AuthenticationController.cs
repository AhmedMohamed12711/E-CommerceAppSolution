
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.IdentityDtos;
using System.Security.Claims;

namespace Presintation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):APIBaseController
    {
        #region Register
        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }
        #endregion
        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var User=await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }
        #endregion
        #region CheckEmail
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>>CheckEmail(string Email)
        {
             var Result=await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }
        #endregion
        #region GetCurrentUser
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>>GetCurrentUser()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUser(Email!);
            return Ok(AppUser);
        }
        #endregion
        #region GetCurrentUserAddress
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Address=await _serviceManager.AuthenticationService.GetCurrentUserAddress(Email!);
            return Ok(Address);
        }
        #endregion
        #region UpdateUserAddress
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var UpdateAddress=await _serviceManager.AuthenticationService.UpdateCurrentUserAddress(addressDto,Email!);
            return Ok(UpdateAddress);
        }

        #endregion
    }
}
