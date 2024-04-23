using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.DomainServices
{
    public class UserDomainService : IDomainService<UserVM, User>
    {
        private AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserDomainService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User Add(UserVM obj) 
        {
            User newUser = new User()
            { 
                UserRole = _context.UserRoles.FirstOrDefault(x => x.UserRoleName.Equals("Registered")),
                UserName = obj.UserName,
                UserSurname = obj.UserSurname,
                UserPhoto = ImageProcessor.ImageToByte(obj.UserPhoto),
                Email = obj.Email,
                Password = HashPassword(obj?.Password),
                PhoneNumber = obj?.PhoneNumber,
                Organisation = obj?.Organisation,
                Speciality = obj?.Speciality,
                City = obj?.City,
                Rating = 0
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }


        public User? FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email.Equals(email));
        }   
        public UserVM ConvertToVm(User obj)
        {
            return new UserVM()
            {
                UserId = obj.UserId,
                UserName = obj.UserName,
                UserSurname = obj.UserSurname,
                City = obj.City,
                Organisation = obj.Organisation,
                Speciality = obj.Speciality,
                UserPhotoBase64 = ImageProcessor.ByteToBase64(obj.UserPhoto),
                DateJoined = obj.CreateDate?.ToString("yyyy-MM-dd")
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public float GetRating(int userId)
        {
            throw new NotImplementedException("Get rating is not implemented yet.");
        }

        private string HashPassword(string ?password)
        {
            using (var sha = SHA256.Create())
            {
                var asByteArray = Encoding.UTF8.GetBytes(password);
                var hashedPassword = sha.ComputeHash(asByteArray);
                return Convert.ToBase64String(hashedPassword);
            }
        }

        public string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:SecretKey").Value);

            // token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", value: user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, value: user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString()),
                    new Claim("Role", value: user.UserRole.UserRoleName)
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token); // converting SecurityToken to a
        }
    }
}
