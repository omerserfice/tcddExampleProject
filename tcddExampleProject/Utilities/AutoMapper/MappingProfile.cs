using AutoMapper;
using tcddExampleProject.Models;
using tcddExampleProject.Models.DTO;

namespace tcddExampleProject.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUserDto, User>();
            CreateMap<User, GetAllUserDto>();
        }
    }
}
