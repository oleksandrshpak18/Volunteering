using AutoMapper;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Data.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserVM>()
            .ForMember(dest => dest.UserPhotoBase64, opt => opt.MapFrom(src => ImageProcessor.ByteToBase64(src.UserPhoto)))
            .ForMember(dest => dest.UserPhotoPassportBase64, opt => opt.Ignore())
            .ForMember(dest => dest.DateJoined, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            //.ForMember(dest => dest.Email, opt => opt.Ignore())
            //.ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            .ForMember(dest => dest.CardNumber, opt => opt.Ignore())
            .ForMember(dest => dest.UserPhoto, opt => opt.Ignore())
            .ForMember(dest => dest.UserPhotoPassport, opt => opt.Ignore())
            
            ;

            CreateMap<UserRegisterRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashProcessor.HashPassword(src.Password)))
                ;

            CreateMap<UserDetailsVM, User>()
                .ForMember(dest => dest.UserPhotoPassport, opt => opt.MapFrom(src => ImageProcessor.ImageToByte(src.UserPhotoPassport)))
                .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => ImageProcessor.ImageToByte(src.UserPhoto)))
                ;

            CreateMap<User, UserPublicInfoVM>()
            .ForMember(dest => dest.UserPhotoBase64, opt => opt.MapFrom(src => ImageProcessor.ByteToBase64(src.UserPhoto)))
            .ForMember(dest => dest.DateJoined, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            ;
        }
    }
}
