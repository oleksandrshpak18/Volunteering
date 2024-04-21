using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Data;
using Volunteering.Data.Interfaces;
using Volunteering.Data.DomainServices;

namespace Volunteering.ApplicationServices
{
    public class NewsApplicationService
    {
        private NewsDomainService _domainService;
        public NewsApplicationService(NewsDomainService domainService)
        {
            _domainService = domainService;
        }
        public IEnumerable<NewsVM> GetAll()
        {
            return _domainService.GetAll().Select(x => _domainService.ConvertToVm(x));
        }

        public NewsVM Add(NewsVM vm)
        {
            return _domainService.ConvertToVm(_domainService.Add(vm));
        }
    }
}
