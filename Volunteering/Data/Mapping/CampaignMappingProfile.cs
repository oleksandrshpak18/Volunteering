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
    .ForMember(dest => dest.CreateDateString, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")))
    .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.FinishDate.ToString("yyyy-MM-dd")))
    .ForMember(dest => dest.FinishDateString, opt => opt.MapFrom(src => src.FinishDate.ToString("yyyy-MM-dd")))
    .ForMember(dest => dest.CampaignStatus, opt => opt.MapFrom(src => src.CampaignStatus.StatusName))
    .ForMember(dest => dest.CampaignPriority, opt => opt.MapFrom(src => src.CampaignPriority.PriorityValue))
    .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.UserCampaigns.First().User.UserName} {src.UserCampaigns.First().User.UserSurname}"))
    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserCampaigns.First().UserId))
    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => $"{src.Subcategory.CategorySubcategories.First(x => x.SubcategoryId == src.SubcategoryId).Category.CategoryName}"))
    .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory.SubcategoryName))
    .ForMember(dest => dest.Report, opt => opt.MapFrom((src, dest) => src.Report != null ? new ReportVM
    {
        CreateDateString = src.Report.CreateDate.ToString("yyyy-MM-dd"),
        ReportName = src.Report.ReportName,
        ReportDescription = src.Report.ReportDescription,
        ReportId = src.Report.ReportId,
        CampaignId = src.CampaignId,
        ReportPhotosBase64 = src.Report.ReportReportPhotos.Select(y => ImageProcessor.ByteToBase64(y.ReportPhoto.Photo)).ToList()
    } : null))
    .ForPath(dest => dest.Report.ReportPhotos, opt => opt.Ignore());






            CreateMap<CampaignVM, Campaign>()
                .ForMember(dest => dest.CampaignPhoto, opt => opt.MapFrom(src => ImageProcessor.ImageToByte(src.CampaignPhoto)))
                //.ForMember(dest => dest.CampaignStatus, opt => opt.Ignore())
                .ForMember(dest => dest.CampaignPriority, opt => opt.Ignore())
                .ForMember(dest => dest.Subcategory, opt => opt.Ignore())

                ;
                
        }
    }
}
