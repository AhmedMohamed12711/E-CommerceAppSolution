using DomainLayer.Exceptions;
using DomainLayer.Models.Identity_Module;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOS.IdentityDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //Check Email is Exists
            var User=await _userManager.FindByEmailAsync(loginDto.Email);
            if(User is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            //CheckPassward
            var IsPasswardValid =await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswardValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email!,
                    Token =await CreateTokenAsync(User)
                };
            
            }
            else
            {
                throw new UnauthorizeException();
            }

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Mapping registerDto to userDto
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
            };
            //Create User
            var Result =await _userManager.CreateAsync(User,registerDto.Password);
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await CreateTokenAsync(User)
                };

            }
            else
            {
                var Errors=Result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }
        private async Task<string> CreateTokenAsync(ApplicationUser User)
        {
            var Claims=new List<Claim>
            {
                new(ClaimTypes.Email,User.Email!),
                new(ClaimTypes.Name,User.UserName!),
                new(ClaimTypes.NameIdentifier,User.Id!),
            };
            var Roles = await _userManager.GetRolesAsync(User);
            foreach (var role in Roles)
            {
                Claims.Add(new(ClaimTypes.Role, role));
            }
                //------------------
                var SecretKey = _configuration.GetSection("JWTOptions")["Secretkey"] ;
                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
                var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                //-----
                var Token = new JwtSecurityToken(
                    issuer: _configuration["JWTOptions.Issuer"],
                    audience: _configuration["JWTOptions.Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: Creds
                    );
            
                return new JwtSecurityTokenHandler().WriteToken(Token);
               
        }

    }
}
