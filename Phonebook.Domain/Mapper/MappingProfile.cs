using AutoMapper;
using PhoneBook.Domain.DTO;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {        
            CreateMap<PhonebookDTO, PhonebookEntity>().ReverseMap();
            CreateMap<EntryDTO, EntryEntity>().ReverseMap();
        }
    }
}
