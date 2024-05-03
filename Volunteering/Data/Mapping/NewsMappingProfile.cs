
using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.Mapping
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            CreateMap<News, NewsVM>()
               .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.User.UserName} {src.User.UserSurname}")) // Map author's name from User object
               .ForMember(dest => dest.NewsPhotoBase64, opt => opt.MapFrom(src => ImageProcessor.ByteToBase64(src.NewsPhoto))) // Convert photo to base64 string
               .ForMember(dest => dest.NewsPhoto, opt => opt.Ignore())
               .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")))
               ;
            
            CreateMap<NewsVM, News>()
                .ForMember(dest => dest.NewsPhoto, opt => opt.MapFrom(src => ImageProcessor.ImageToByte(src.NewsPhoto))) // Convert base64 back to byte 
                ;
        }
    }
}
