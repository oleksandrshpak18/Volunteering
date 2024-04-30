using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Data;
using Volunteering.Data.Interfaces;
using Volunteering.Data.DomainServices;

namespace Volunteering.ApplicationServices
{
    public class CampaignStatusApplicationService
    { 
        private CampaignStatusDomainService _domainService;
        public CampaignStatusApplicationService(CampaignStatusDomainService domainService)
        {
            _domainService = domainService;
        }
        public IEnumerable<CampaignStatusVm> GetAll()
        {
            return _domainService.ModelToVm(_domainService.GetAll());
        }

        public CampaignStatusVm Add(CampaignStatusVm vm)
        {
            return _domainService.ModelToVm(_domainService.Add(vm));
        }

        public CampaignStatusVm Update(CampaignStatusVm vm)
        {
            var res = _domainService.Update(vm);
            if(res == null)
            {
                return null;
            }

            return _domainService.ModelToVm(res);
        }

        public bool Delete(CampaignStatusVm vm)
        {
            return _domainService.Delete(vm);
        }
    }
}
