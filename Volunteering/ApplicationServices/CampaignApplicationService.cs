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

        public IEnumerable<CampaignVM> GetAll(CampaignFilter ?filter, string? sortBy, bool isDescending=true, int page=1, int pageSize=8)
        {
            return _domainService.ModelToVm(_domainService.GetAll(filter, sortBy, isDescending, page, pageSize));
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

        public IEnumerable<CampaignVM>? GetNew(int page = 1)
        {
            var filter = new CampaignFilter
            {
                Status = "Новий"
            };

            var res = _domainService.GetAll(filter, pageSize: 10, page: page, isAdmin: true, sortBy: "createDate");

            var resTmp = _domainService.ModelToVm(res);

            return resTmp;

            //return _domainService.ModelToVm(_domainService.GetAll(filter, pageSize: 10, page: page, isAdmin: true));
        }

        public CampaignStatusVm UpdateStatus(CampaignStatusUpdateRequest req)
        {
            var res = _domainService.UpdateStatus(req);
            return new CampaignStatusVm { CampaignStatusId =res.CampaignStatusId, StatusDescription = res.StatusDescription , StatusName = res.StatusName};
        }

        public IEnumerable<CampaignVM> GetRecent(int count)
        {
            return _domainService.ModelToVm(_domainService.GetRecent(count));
        }

        public CampaignVM AddReport(Guid userId, ReportVM vm)
        {

            return _domainService.ModelToVm(_domainService.AddReport(userId, vm));
        }

        public StatisticsResponse GetStatistics()
        {
            return _domainService.GetStatistics();
        }

        public CampaignVM? GetById(Guid id, Guid? userId = null)
        {
            return _domainService.ModelToVm(_domainService.Get(id, userId));
        }

        public IEnumerable<CampaignVM> GetByUserId(Guid userId, bool isOwner=false)
        {
            return _domainService.ModelToVm(_domainService.GetByUserId(userId, isOwner));
        }

        public FullStatisticsResponse GetFullStatistics()
        {
            NumberStatistics numberStat = _domainService.GetNumberStatistics();
            List< CategoryStatistics> categoryStatistics = _domainService.GetCategoryStatistics();

            return new FullStatisticsResponse
            {
                NumberStatistics = numberStat,
                CategoriesStatistics = categoryStatistics
            };
        }
    }
}
