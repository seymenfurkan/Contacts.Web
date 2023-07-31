using AutoMapper;
using Contacts.Core.Entities.DTOs;
using Contacts.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Business.Mapping.ForPerson
{
    public class DtoMapping : Profile
    {
        public DtoMapping()
        {
            CreateMap<Person, PersonDetailDto>().ReverseMap();
            CreateMap<CreatePersonDto, Person>().ReverseMap();
            CreateMap<DeletePersonDto, Person>();
            CreateMap<DeletePersonDto, PersonDetailDto>().ReverseMap();
            CreateMap<UpdatePersonDto, Person>().ReverseMap();
            CreateMap<GetPersonByIdDto, DeletePersonDto>().ReverseMap();
            CreateMap<GetPersonByIdDto, PersonDetailDto>().ReverseMap();
            CreateMap<GetPersonByIdDto, UpdatePersonDto>().ReverseMap();
            CreateMap<GetPersonByIdDto, Person>().ReverseMap();
            CreateMap<Person, PersonListDto>().ReverseMap();

        }
    }
}
