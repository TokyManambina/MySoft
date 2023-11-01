using AutoMapper;
using SoftSignAPI.Dto;
using SoftSignAPI.Model;

namespace SoftSignAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<User, User>();
            CreateMap<Society, SocietyDto>();
            CreateMap<SocietyDto, Society>();
            CreateMap<User, UserDto>();
            CreateMap<AuthenticationDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, string>();
            CreateMap<string, User>();
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();

            CreateMap<Document, DocumentDto>();
            CreateMap<DocumentDto, Document>();
            CreateMap<ShowDocument, Document>();
            CreateMap<Document, ShowDocument>();

            CreateMap<DocumentByUserDto, UserDocument>();
            CreateMap<UserDocument, DocumentByUserDto>();

        }
    }
}
