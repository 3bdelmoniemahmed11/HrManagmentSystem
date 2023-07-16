using AuthenticationWithIdentity.Data;
using AuthenticationWithIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationWithIdentity.Services
{
    public class TokenService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        private readonly ApplicationContext _context;
        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthModel> GenerateTokenAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                // Handle invalid credentials
                return null;
            }
            var token = await GenerateAuthToken(user);
            return token;
        }
        private async Task<AuthModel> GenerateAuthToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                  new Claim(ClaimTypes.GroupSid, user.GroupId.ToString()),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            List<object> RoleActions = new List<object>();
            foreach (var userRole in userRoles)
            {
                RoleActions.Add(new
                {
                    Name = userRole,
                    Actions = _context.ApplicationRoles.Include(x=>x.Role).Include(x=>x.PageAction).Where(x => x.Role.Name.Equals(userRole)).ToList()
                });


                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1000),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new AuthModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Permissions = RoleActions
            };
        }
    }
}
