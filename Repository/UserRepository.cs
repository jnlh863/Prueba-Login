using MealMasterAPI.Data;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
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

        public User GetUser(int userid)
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

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.password, user.password)) {
                throw new NotImplementedException("User not found or invalidad password.");
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

        public string UpdateRol(int id, string role)
        {
            var user = _bd.Users.Find(id);

            if (user == null)
            {
                return "User not found.";
            }
            user.Role = role;
           
            return "Role assigned successfully.";
        }

        public string UpdateUser(int id, UserDTO userdto)
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
        
        public string DeleteUser(int userid)
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
    }
}
