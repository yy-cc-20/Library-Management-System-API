using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace LibraryManagementSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}
