using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
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
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Data.DomainServices
{
    public class UserDomainService
    {
        private AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserDomainService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User Register(UserRegisterRequest obj) 
        {
            if(obj == null) { throw new ArgumentNullException(nameof(obj)); }

            User newUser = new User()
            { 
                UserRole = _context.UserRoles.FirstOrDefault(x => x.UserRoleName.Equals("Registered")),
                UserName = obj.UserName,
                UserSurname = obj.UserSurname,
                UserPhoto = ImageProcessor.ImageToByte(obj.UserPhoto),
                Email = obj.Email,
                Password = HashPassword(obj.Password),
                Rating = 0
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public bool VerifyPassword(User? user, string enteredPassword)
        {
            if (user == null) return false;
            return BCrypt.Net.BCrypt.Verify(enteredPassword, user?.Password);
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
                    new Claim(ClaimTypes.Role, value: user.UserRole.UserRoleName)
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token); // converting SecurityToken to a
        }

        public User? FindByEmail(string email)
        {
            User ?user = _context.Users.FirstOrDefault(x => x.Email.Equals(email));
            if (user != null)
            {
                UserRole ?role = _context.UserRoles.FirstOrDefault(x => x.Users.Contains(user));
                if (role != null)
                {
                    user.UserRole = role;
                    return user;
                }
            }
            return null;
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
                DateJoined = obj.CreateDate?.ToString("yyyy-MM-dd"),
                Rating = obj.Rating
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

        public bool? IsInfoFilled(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return null;
            }

            return user.UserPhotoPassport != null
                && user.CardNumber != null
                && user.UserPhoto != null
                && user.City != null
                && user.Organisation != null
                && user.Speciality != null
                && user.PhoneNumber != null
                && user.UserName != null
                && user.UserSurname != null;
        }

        public User? Update (int userId, UserDetailsVM newUser)
        {
            User? user = _context.Users.Find(userId);

            if (user != null)
            {
                user.PhoneNumber = newUser.PhoneNumber;
                user.Organisation = newUser.Organisation;
                user.Speciality = newUser.Speciality;
                user.City = newUser.City;
                user.UserPhotoPassport = ImageProcessor.ImageToByte(newUser.UserPhotoPassport);
                user.CardNumber = newUser.CardNumber;

                _context.SaveChanges();
            }

            return user;
        }
    }
}
