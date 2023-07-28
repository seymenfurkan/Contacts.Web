using AutoMapper;
using Contacts.Core.Entities.DTOs;
using Contacts.Web.Models.PersonViewModels;

namespace Contacts.Web.Mapping.ForPerson
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<PersonListDto, PersonListViewModel>().ReverseMap();

            CreateMap<CreatePersonViewModel, CreatePersonDto>();

            CreateMap<UpdatePersonViewModel, UpdatePersonDto>().ReverseMap();
            CreateMap<UpdatePersonViewModel, GetPersonByIdDto>().ReverseMap();

            CreateMap<DeletePersonViewModel, DeletePersonDto>();
            CreateMap<GetPersonWithDetailByIdViewModel, GetPersonByIdDto>().ReverseMap();
            CreateMap<DeletePersonDto, DeletePersonViewModel>();

            CreateMap<PersonDetailDto, GetPersonWithDetailByIdViewModel>().ReverseMap();
        }
    }
}
