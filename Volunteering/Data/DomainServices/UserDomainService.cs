using Volunteering.Data.Interfaces;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.DomainServices
{
    public class UserDomainService : IDomainService<UserVM, User>, IImageProcessing
    {
        private AppDbContext _context;
        public UserDomainService(AppDbContext context)
        {
            _context = context;
        }

    }
}
