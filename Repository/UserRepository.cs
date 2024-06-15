using Azure.Core;
using MealMasterAPI.Data;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
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

        public User CreateUser(RegisterDTO user)
        {
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
            return us;

        }

        public User GetUser(Guid userid)
        {
            var user = _bd.Users.Find(userid);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
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
                Issuer = "mealmasterapi",
                Audience = "mealmaster.com",
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tok.CreateToken(tokenDescriptor);

            UserTokenDTO ut = new UserTokenDTO()
            {
                Token = tok.WriteToken(token)
            };

            return ut;

        }


        public UserTokenDTO RequestPasswordReset(ResetPassDTO request) 
        {
            var user = _bd.Users.Find(request.email);

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
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tok.CreateToken(tokenDescriptor);

            UserTokenDTO ut = new UserTokenDTO()
            {
                Token = tok.WriteToken(token)
            };

            return ut;
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

        /*public void SendPasswordResetEmail(string email, string resetLink)
        {
            var client = new SendGridClient("your_api_key");
            var from = new EmailAddress("no-reply@yourdomain.com", "Your App Name");
            var subject = "Password Reset";
            var to = new EmailAddress(email);
            var plainTextContent = $"Click the link to reset your password: {resetLink}";
            var htmlContent = $"<a href='{resetLink}'>Click here to reset your password</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            client.SendEmailAsync(msg).Wait();
        }*/

    }
}
