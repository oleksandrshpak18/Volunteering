using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.Mapping
{
    public class ClassifiersMappingProfile : Profile
    {
        public ClassifiersMappingProfile()
        {
            CreateMap<CampaignStatus, CampaignStatusVm>();
            CreateMap<CampaignStatusVm, CampaignStatus>();

            CreateMap<CampaignPriority, CampaignPriorityVm>();
            CreateMap<CampaignPriorityVm, CampaignPriority>();

            CreateMap<Category, CategoryVM>()
                .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.CategorySubcategories.Select(cs => cs.Subcategory.SubcategoryName).ToList()));
            ;

        }

    }
}
