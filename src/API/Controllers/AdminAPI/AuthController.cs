using API.Extensions;
using API.Services;
using Core.Domain.Identity;
using Core.Models.Auth;
using Core.Models.Systems;
using Core.SeedWorks.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace API.Controllers.AdminAPI
{
    [Route("api/admin/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<AppRole> _roleManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedResult>> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || user.IsActive==false||user.LockoutEnabled)
            {
                return Unauthorized("Invalid username or password.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var permissions = await this.GetUserPermissionsAsync(user.Id.ToString());
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(UserClaims.Id, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName ?? string.Empty),
                new Claim(UserClaims.FirstName, user.FirstName ?? ""),
                new Claim(UserClaims.Roles, string.Join(",", roles)),
                new Claim(UserClaims.Permissions, JsonSerializer.Serialize(permissions)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthenticatedResult
            {
                Token = accessToken,
                refreshToken = refreshToken
            });
        }

        private async Task<List<string>> GetUserPermissionsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = new List<string>();

            var allpermissions = new List<RoleClaimsDTO>();
            if(roles.Contains(Roles.Admin))
            {
                var types = typeof(Permissions).GetTypeInfo().DeclaredNestedTypes;
                foreach (var type in types)
                {
                    allpermissions.GetPermissions(type);
                }
                permissions.AddRange(allpermissions.Select(p => p.Value));

            }
            else
            {
                foreach (var roleName in roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    var claims = await _roleManager.GetClaimsAsync(role);
                    var roleClaimsValues = claims.Select(c => c.Value).ToList();
                    permissions.AddRange(roleClaimsValues);
                }
            }

            return permissions.Distinct().ToList();


        }

    }
}
