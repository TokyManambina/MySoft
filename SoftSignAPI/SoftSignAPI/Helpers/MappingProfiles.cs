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
            CreateMap<UserDto, User>();
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();
            CreateMap<Document, DocumentDto>();
            CreateMap<DocumentDto, DocumentDto>();

        }
    }
}
