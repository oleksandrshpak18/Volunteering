using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Data;
using Volunteering.Data.Interfaces;
using Volunteering.Data.DomainServices;

namespace Volunteering.ApplicationServices
{
    public class NewsApplicationService : IApplicationService<NewsVM>
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

        public NewsVM Add(int id, NewsVM vm)
        {
            return _domainService.ConvertToVm(_domainService.Add(id, vm));
        }

        public NewsVM Update(NewsVM vm)
        {
            var res = _domainService.Update( vm);
            if(res == null)
            {
                return null;
            }

            return _domainService.ConvertToVm(res);
        }
    }
}
