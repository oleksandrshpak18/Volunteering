using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Data.Mapping
{
    public class DonationMapperProfile : Profile
    {
        public DonationMapperProfile()
        {
            CreateMap<Donation, DonationVM>()
                .ForMember(dest => dest.CampaignId, opt => opt.MapFrom(src => src.CampaignId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DonationValue, opt => opt.MapFrom(src => src.DonationValue))
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
