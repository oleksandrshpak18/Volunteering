using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.Mapping
{
    public class DonationMappingProfile : Profile
    {
        public DonationMappingProfile()
        {
            CreateMap<Donation, DonationVM>()
                .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => src.CampaignId))
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.Campaign.CampaignName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DonationValue, opt => opt.MapFrom(src => src.DonationValue))
                .ForMember(dest => dest.DonationDate, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")))
               ;

            CreateMap<DonationVM, Donation>()
                 .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => src.CampaignId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DonationValue, opt => opt.MapFrom(src => src.DonationValue))
               ;

            ;
        }
    }
}
