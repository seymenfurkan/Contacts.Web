using Contacts.Core.Entities.DTOs;
using Contacts.Core.Utilities.Results.Abstract;
using Contacts.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Business.Abstract
{
    //DTO !
    public interface IPersonService
    {
        IDataResult<List<PersonListDto>> GetAllPeople();
        IDataResult<GetPersonByIdDto> GetPerson(int id);
        IResult AddPerson(CreatePersonDto entity);
        IResult DeletePerson(PersonDetailDto entity);
        IResult UpdatePerson(UpdatePersonDto entity);
        IDataResult<PersonDetailDto> GetPersonDetail(int id);
        IDataResult<List<PersonListDto>> GetQueryabla(int index, int size);
    }
}
