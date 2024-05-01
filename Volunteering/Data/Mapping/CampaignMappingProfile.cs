using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.Mapping
{
    public class CampaignMappingProfile : Profile
    {
        public CampaignMappingProfile()
        {
            CreateMap<Campaign, CampaignVM>()
                .ForMember(dest => dest.CampaignPhotoBase64, opt => opt.MapFrom(src => ImageProcessor.ByteToBase64(src.CampaignPhoto)))
                .ForMember(dest => dest.CampaignPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.FinishDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CampaignStatus, opt => opt.MapFrom(src => src.CampaignStatus.StatusName))
                .ForMember(dest => dest.CampaignPriority, opt => opt.MapFrom(src => src.CampaignPriority.PriorityValue))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.UserCampaigns.First().User.UserName} {src.UserCampaigns.First().User.UserSurname}"))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserCampaigns.First().User.UserId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => $"{src.Subcategory.CategorySubcategories.First(x => x.SubcategoryId == src.SubcategoryId).Category.CategoryName}"))
                .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory.SubcategoryName))
                .ForPath(dest => dest.Report.CreateDate, opt => opt.MapFrom(src => src.Report.CreateDate.ToString("yyyy-MM-dd")))
                .ForPath(dest => dest.Report.ReportPhotos, opt => opt.Ignore())
                .ForPath(dest => dest.Report.ReportName, opt => opt.MapFrom(src => src.Report.ReportName))
                .ForPath(dest => dest.Report.ReportDescription, opt => opt.MapFrom(src => src.Report.ReportDescription))
                .ForPath(dest => dest.Report.ReportId, opt => opt.MapFrom(src => src.Report.ReportId))
                .ForPath(dest => dest.Report.CampaignId, opt => opt.MapFrom(src => src.CampaignId))
                .ForPath(dest => dest.Report.ReportPhotosBase64, opt => opt.MapFrom(x => x.Report.ReportReportPhotos.Select(y => ImageProcessor.ByteToBase64(y.ReportPhoto.Photo))))
                ;

            CreateMap<CampaignVM, Campaign>()
                .ForMember(dest => dest.CampaignPhoto, opt => opt.MapFrom(src => ImageProcessor.ImageToByte(src.CampaignPhoto)))
                //.ForMember(dest => dest.CampaignStatus, opt => opt.Ignore())
                .ForMember(dest => dest.CampaignPriority, opt => opt.Ignore())
                .ForMember(dest => dest.Subcategory, opt => opt.Ignore())

                ;
                
        }
    }
}
