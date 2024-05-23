﻿using Volunteering.Data.DomainServices;
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

        public IEnumerable<CampaignVM>? GetNew()
        {
            var filter = new CampaignFilter
            {
                Status = "Новий"
            };

            return _domainService.ModelToVm(_domainService.GetAll(filter));
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

        public CampaignVM? GetById(Guid id)
        {
            return _domainService.ModelToVm(_domainService.Get(id));
        }

        public IEnumerable<CampaignVM> GetByUserId(Guid userId)
        {
            return _domainService.ModelToVm(_domainService.GetByUserId(userId));
        }
    }
}
