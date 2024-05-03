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
        public IEnumerable<NewsVM> GetPage(int pageNumber)
        {
            return _domainService.ModelToVm(_domainService.GetPage(page: pageNumber));
        }

        public IEnumerable<NewsVM> GetAll()
        {
            return _domainService.ModelToVm(_domainService.GetAll());
        }

        public NewsVM Add(Guid userId, NewsVM vm)
        {
            vm.UserId = userId;
            return _domainService.ModelToVm(_domainService.Add(vm));
        }

        public NewsVM Update(Guid userId, NewsVM vm)
        {
            vm.UserId = userId;
            var res = _domainService.Update(vm);
            if(res == null)
            {
                return null;
            }

            return _domainService.ModelToVm(res);
        }

        public IEnumerable<NewsVM> GetRecent(int ?count)
        {
            if (count == null)
            { 
                return _domainService.ModelToVm(_domainService.GetRecent()); 
            }
            return _domainService.ModelToVm(_domainService.GetRecent(count.Value));
        }

        public NewsVM GetById(Guid id)
        {
            return _domainService.ModelToVm(_domainService.Get(id));
        }
    }
}
