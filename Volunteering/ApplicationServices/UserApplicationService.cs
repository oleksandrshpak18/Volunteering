using Volunteering.Data.DomainServices;
using Volunteering.Data.Interfaces;
using Volunteering.Data.ViewModels;

namespace Volunteering.ApplicationServices
{
    public class UserApplicationService : IApplicationService<UserVM>
    {
        private UserDomainService _domainService;
        public UserApplicationService(UserDomainService domainService)
        {
            _domainService = domainService;
        }

        public UserVM Add (UserVM vm)
        {
            return _domainService.ConvertToVm(_domainService.Add(vm));
        }

        public IEnumerable<UserVM> GetAll()
        {
            return _domainService.GetAll().Select(x => _domainService.ConvertToVm(x));
        }
    }
}
