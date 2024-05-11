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
using AutoMapper;
using System.Collections.Generic;

namespace Volunteering.Data.DomainServices
{
    public class UserDomainService
    {
        private AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserDomainService(AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public UserVM ModelToVm(User user) => _mapper.Map<UserVM>(user);

        //public IEnumerable<UserShortInfoVM> ModelToShortVm(IEnumerable<User> userList) => _mapper.Map<List<UserShortInfoVM>>(userList);

        public List<UserVM> ModelToVm(IEnumerable<User> userList) => _mapper.Map<List<UserVM>>(userList);
        
        public User VmToModel(UserVM vm) => _mapper.Map<User>(vm);
        
        public User RegisterVmToModel(UserRegisterRequest vm) => _mapper.Map<User>(vm);

        public User Add(UserRegisterRequest obj) 
        {
            if(obj == null) { throw new ArgumentNullException(nameof(obj)); }

            User res = RegisterVmToModel(obj);
            res.UserRole = _context.UserRoles.FirstOrDefault(x => x.UserRoleName.Equals("Registered"));
            _context.Users.Add(res);
            _context.SaveChanges();

            return res;
        }

        public bool VerifyPassword(User existingUser, string enteredPassword)
        {
            return HashProcessor.VerifyPassword(existingUser.Password, enteredPassword);
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
                    //new Claim(JwtRegisteredClaimNames.Sub, value: user.Email),
                    //new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
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

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public bool? IsInfoFilled(Guid userId)
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
                && user.UserSurname != null
                && user.UserPhoto != null;
        }
        
        public User? Update (UserDetailsVM newUser)
        {
            User? res = _context.Users.Find(newUser.UserId);
            _mapper.Map(newUser, res);
            _context.SaveChanges();
            return res;
        }

        public List<UserShortInfoVM> GetTop(int count = 5)
        {
            var topUsers = _context.Users
                .Where(x => x.UserRole.UserRoleName != "Admin")
                .Select(u => new UserShortInfoVM
                {
                    UserId = u.UserId,
                    FullName = $"{u.UserName} {u.UserSurname}", // Concatenate names
                    Accumulated = u.UserCampaigns.Sum(c => c.Campaign.Accumulated ?? 0), // Pre-compute accumulated sum
                    ReportCount = u.UserCampaigns.Select(c => c.Campaign).Where(y => y.Report != null).Count(), // Count the number of reports
                    UserPhotoBase64 = ImageProcessor.ByteToBase64(u.UserPhoto)
                })
                .OrderByDescending(u => u.Accumulated) // Order by accumulated sum descending
                .Take(count) // Take the specified number of users
                .ToList();

            return topUsers; // Return the precomputed list
        }

        internal UserShortInfoVM GetShortInfo(Guid userId)
        {
            var user = _context.Users.Find(userId);
            if(user == null) { return null; }

            return new UserShortInfoVM()
                {
                    UserId = user.UserId,
                    FullName = $"{user.UserName} {user.UserSurname}", // Concatenate names
                    Accumulated = user.UserCampaigns.Sum(c => c.Campaign.Accumulated ?? 0), // Pre-compute accumulated sum
                    ReportCount = user.UserCampaigns.Select(c => c.Campaign).Where(y => y.Report != null).Count(), // Count the number of reports
                    UserPhotoBase64 = ImageProcessor.ByteToBase64(user.UserPhoto)
                };
        }

        public User? GetById(Guid userId)
        {
            return _context.Users.Find(userId);
        }
    }
}
