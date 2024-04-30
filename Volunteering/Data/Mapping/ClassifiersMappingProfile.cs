using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.Mapping
{
    public class ClassifiersMappingProfile : Profile
    {
        public ClassifiersMappingProfile()
        {
            CreateMap<CampaignStatus, CampaignStatusVm>();
            CreateMap<CampaignStatusVm, CampaignStatus>();
        }
    }
}
