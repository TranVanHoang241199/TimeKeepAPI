using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimekeeperAPI.Common.Utils;
using TimekeeperAPI.Data.Data.DbContexts;

namespace TimekeeperAPI.Business.Services.Auth
{
    /// <summary>
    /// lớp xử lý phân quyền 
    /// </summary>
    public class AuthHandler : IAuthHandler
    {
        private readonly TkDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthHandler(TkDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Response> Authenticate(AuthUser userCred)
        {
            try
            {
                var userToAccount = new UserAccountViewModel();

                //kiem tra tai khoan mat khau dung hay khong
                var user = await _context.Tk_Users.Where(u => u.Name.Equals(userCred.Name) & u.Password.Equals(userCred.Password)).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new Response(HttpStatusCode.BadRequest, "incorrect information");
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Id", user.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signIn
                    );

                //gan cho viewModel
                userToAccount.Token = new JwtSecurityTokenHandler().WriteToken(token).ToString();
                user.LastLogin = DateTime.Now;
                userToAccount.Name = user.Name;
                userToAccount.Id = user.Id;
                userToAccount.LastLogin = user.LastLogin;
                userToAccount.Role = user.Role;

                return new ResponseObject<UserAccountViewModel>(userToAccount, "Account success.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Error("userCred: {@userCred}", userCred);
                return new Response<AuthUser>
                {
                    Data = null,
                    Message = ex.Message,
                    Code = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
