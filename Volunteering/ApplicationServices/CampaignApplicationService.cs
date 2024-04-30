using Volunteering.Data.DomainServices;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.ApplicationServices
{
    public class CampaignApplicationService
    {
        private CampaignDomainService _domainService;
        public CampaignApplicationService(CampaignDomainService domainService)
        {
            _domainService = domainService;
        }

        public IEnumerable<CampaignVM> GetAll(CampaignFilter ?filter)
        {
            return _domainService.ModelToVm(_domainService.GetAll(filter));
        }

        public Response<CampaignVM> Add(Guid userId, CampaignVM vm)
        {
            vm.UserId = userId;

            try
            {
                var res = _domainService.Add(vm);
                if (res != null)
                {
                    var campaignVM = _domainService.ModelToVm(res);
                    return new Response<CampaignVM>
                    {
                        Data = campaignVM
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<CampaignVM>
                {
                    Error = ex.Message
                };
            }

            return new Response<CampaignVM>
            {
                Error = "Unknown error or campaign could not be created."
            };
        }

        public IEnumerable<CampaignVM>? GetNew()
        {
            var filter = new CampaignFilter
            {
                Status = "Новий"
            };

            return _domainService.ModelToVm(_domainService.GetAll(filter));
        }

        public bool UpdateStatus(CampaignStatusUpdateRequest req)
        {
            var res = _domainService.UpdateStatus(req);
            return res != null;
        }
    }
}
