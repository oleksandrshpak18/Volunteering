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

        public IEnumerable<CampaignVM> GetAll()
        {
            return _domainService.ModelToVm(_domainService.GetAll());
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
    }
}
