using AutoMapper;
using Contacts.Business.Abstract;
using Contacts.Core.Entities.DTOs;
using Contacts.Core.Utilities.Results.Abstract;
using Contacts.Core.Utilities.Results.Concrete;
using Contacts.DataAccess.Abstract;
using Contacts.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Business.Concrete
{
    //DTO !
    public class PersonManager : IPersonService
    {

        private readonly IPersonDal _personDal;
        private readonly IMapper _mapper;

        public PersonManager(IPersonDal personDal, IMapper mapper)
        {
            _personDal = personDal;
            _mapper = mapper;
        }

        public IResult AddPerson(CreatePersonDto entity)
        {
            if (DateTime.Now.Hour is 07)
            {
                return new ErrorResult("Maintenance time !");
            }
            var mappedEntity = _mapper.Map<Person>(entity);
            mappedEntity.CreatedDate = DateTime.Now;
            mappedEntity.UpdatedDate = DateTime.Now;
            _personDal.Add(mappedEntity);
            return new SuccessResult("Kişi başarılı bir şekilde eklendi !");
        }

        public IResult DeletePerson(PersonDetailDto entity)
        {
            if (DateTime.Now.Hour is 07)
            {
                return new ErrorResult("Maintenance time !");
            }
            var mappedEntity = _mapper.Map<Person>(entity);
            mappedEntity.IsDeleted = true;
            _personDal.Update(mappedEntity);
            //_personDal.Delete(mappedEntity);
            return new SuccessResult("Kişi başarılı bir şekilde silindi !");
        }


        public IDataResult<List<PersonListDto>> GetAllPeople()
        {
            if (DateTime.Now.Hour is 07)
            {
                return new ErrorDataResult<List<PersonListDto>>("Kişiler bakımdan dolayı listelenemiyor !");
            }
            var products = _personDal.GetAll(x => x.IsDeleted == false);
            var mappedEntities = _mapper.Map<List<PersonListDto>>(products);
            return new SuccessDataResult<List<PersonListDto>>(mappedEntities, "Kişiler başarılı bir şekilde listelendi !");
        }

        public IDataResult<GetPersonByIdDto> GetPerson(int id)
        {
            if (DateTime.Now.Hour is 07)
            {
                return new ErrorDataResult<GetPersonByIdDto>("Bakımdan dolayı kişi sayfasına ulaşılamıyor !");
            }
            var entity = _personDal.Get(p => p.Id == id);
            var mappedEntity = _mapper.Map<GetPersonByIdDto>(entity);

            return new SuccessDataResult<GetPersonByIdDto>(mappedEntity, "Kişi sayfasına başarılı bir şekilde ulaşıldı !");
        }

        public IDataResult <PersonDetailDto> GetPersonDetail(int id)
        {
            if (DateTime.Now.Hour is 07)
            {
                return new ErrorDataResult<PersonDetailDto>("Bakımdan dolayı kişi detay sayfasına ulaşılamıyor !");
            }
            var item = _personDal.Get(p => p.Id == id);
            var mappedItem = _mapper.Map<PersonDetailDto>(item);
            return new SuccessDataResult<PersonDetailDto>(mappedItem, "Kişi detay sayfasına başarılı bir şekilde ulaşıldı !");
        }

        public IDataResult<List<PersonListDto>> GetQueryabla(int index, int size)
        {
            var count = _personDal.GetQueryable().Count();
            IQueryable<Person> result = _personDal.GetQueryable();

            result = result.Skip(index * size).Take(size);
            var listResult = result.ToList();
            if (listResult.Count == 0)
            {
                return new SuccessDataResult<List<PersonListDto>>(null, "no record");
            }
            var mappedResult = _mapper.Map<List<PersonListDto>>(listResult);
            return new SuccessDataResult<List<PersonListDto>>(
                mappedResult,
                "success");

        }

        public IResult UpdatePerson(UpdatePersonDto entity)
        {
            if (DateTime.Now.Hour is 07)
            {
                return new ErrorResult("Maintenance time !");
            }
            var mappedEntity = _mapper.Map<Person>(entity);
            _personDal.Update(mappedEntity);
            return new SuccessResult("Kişi başarılı bir şekilde güncellendi !");
        }
    }
}
