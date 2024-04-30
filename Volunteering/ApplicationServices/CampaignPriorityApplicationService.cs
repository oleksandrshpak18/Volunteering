using Volunteering.Data.DomainServices;
using Volunteering.Data.ViewModels;

namespace Volunteering.ApplicationServices
{
    public class CampaignPriorityApplicationService
    {
        private CampaignPriorityDomainService _domainService;
        public CampaignPriorityApplicationService(CampaignPriorityDomainService domainService)
        {
            _domainService = domainService;
        }
        public IEnumerable<CampaignPriorityVm> GetAll()
        {
            return _domainService.ModelToVm(_domainService.GetAll());
        }

        public CampaignPriorityVm Add(CampaignPriorityVm vm)
        {
            return _domainService.ModelToVm(_domainService.Add(vm));
        }

        public CampaignPriorityVm Update(CampaignPriorityVm vm)
        {
            var res = _domainService.Update(vm);
            if (res == null)
            {
                return null;
            }

            return _domainService.ModelToVm(res);
        }

        public bool Delete(CampaignPriorityVm vm)
        {
            return _domainService.Delete(vm);
        }
    }
}
