using AutoMapper;
using SoftSignAPI.Model;

namespace SoftSignAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<User, User>();
        }
    }
}
