using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.DomainServices
{
    public class UserDomainService : IDomainService<UserVM, User>
    {
        private AppDbContext _context;
        public UserDomainService(AppDbContext context)
        {
            _context = context;
        }

        public User Add(UserVM obj)
        {
            throw new NotImplementedException();
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
    }
}
