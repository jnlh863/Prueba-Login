using Azure.Core;
using MealMasterAPI.Data;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace MealMasterAPI.Repository
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _bd;
        private string clave;

        public UserRepository(AppDbContext bd)
        {
            _bd = bd;
            clave = Environment.GetEnvironmentVariable("TOKEN");
        }

        public string CreateUser(RegisterDTO user)
        {
            if(user == null)
            {
                return "No se guardaron los datos, intentelo de nuevo";
            }

            User us = new User
            {
                username = user.username,
                email = user.email,
                password = BCrypt.Net.BCrypt.HashPassword(user.password),
                created_at = DateTime.Now,
                profile = null
            };

            _bd.Users.Add(us);
            Guardar();
            return "SI";

        }

        public User GetUser(Guid userid)
        {
            var user = _bd.Users.Find(userid);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }

        public ICollection<User> GetUsers()
        {
           
            return _bd.Users.OrderBy(c => c.username).ToList();
            
        }

        public UserTokenDTO LoginUser(LoginDTO loginDTO)
        {
            var user = _bd.Users.FirstOrDefault(
                    u => u.email.ToLower() == loginDTO.email.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.password, user.password))
            {
                throw new Exception("User not found or invalidad password.");
            }

            var tok = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(clave);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.username.ToString()),
                        new Claim(ClaimTypes.Email, user.email.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "logintestapi",
                Audience = "logintest.com",
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tok.CreateToken(tokenDescriptor);
            var u = _bd.Users.FirstOrDefault(u => u.email == loginDTO.email);

            UserTokenDTO ut = new UserTokenDTO()
            {
                Token = tok.WriteToken(token),
                id = u.id
            };

            return ut;

        }

        public UserTokenDTO RequestPasswordResetToken(ForgotPassDTO request) 
        {
            var user = _bd.Users.FirstOrDefault(u => u.email == request.email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var tok = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(clave);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.username.ToString()),
                        new Claim(ClaimTypes.Email, user.email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "logintestapi",
                Audience = "logintest.com",
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tok.CreateToken(tokenDescriptor);

            UserTokenDTO ut = new UserTokenDTO()
            {
                Token = tok.WriteToken(token),
                id = user.id

            };

            return ut;
        }

        public bool ResetPassword(string email, string token, string newpassword)
        {
            var principal = GetPrincipalFromExpiredToken(token);

            if (principal == null)
            {
                return false;
            }

            var user = _bd.Users.FirstOrDefault(u => u.email == email);

            if (user == null)
            {
                return false;
            }

            user.password = BCrypt.Net.BCrypt.HashPassword(newpassword);

            _bd.Users.Update(user);
            Guardar();

            return true;
        }

        public string UpdateRol(Guid id, string role)
        {
            var user = _bd.Users.Find(id);

            if (user == null)
            {
                return "User not found.";
            }
            user.Role = role;
           
            return "Role assigned successfully.";
        }

        public string UpdateUser(Guid id, UserDTO userdto)
        {
            var user = _bd.Users.Find(id);

            if (user == null)
            {
                return "User not found.";
            }

            user.created_at = DateTime.Now;
            user.username = userdto.username;
            user.email = userdto.email;

            _bd.Users.Update(user);
            Guardar();
            return "Update completed";
        }
        
        public string DeleteUser(Guid userid)
        {
            var user = _bd.Users.Find(userid);
            if (user == null)
            {
                return "User not found.";
            }

            _bd.Users.Remove(user);
            Guardar();
            return "User deleted successfully.";
        }
        
        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(clave);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "logintestapi",
                ValidAudience = "logintest.com",
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }


    }
}
