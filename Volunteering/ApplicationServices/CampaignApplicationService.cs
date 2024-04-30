using Volunteering.Data.DomainServices;
using Volunteering.Data.ViewModels;

namespace Volunteering.ApplicationServices
{
    public class CampaignApplicationService
    {
        private CampaignDomainService _domainService;
        public CampaignApplicationService(CampaignDomainService domainService)
        {
            _domainService = domainService;
        }

        public IEnumerable<CampaignStatusVm> GetAll()
        {
            var res = _domainService.GetAll();
            
            return null;
        }

        public CampaignVM? Add(Guid userId, CampaignVM vm)
        {
            //var res = _domainService.Add(userId, vm);
            return null;
        }
    }
}
