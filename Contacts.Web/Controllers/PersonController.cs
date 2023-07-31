using AutoMapper;
using Contacts.Business.Abstract;
using Contacts.Core.Entities.DTOs;
using Contacts.DataAccess.Concrete.EntityFrameworkCore;
using Contacts.Entities.Concrete;
using Contacts.Web.Models.PersonViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Web.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _service;
        private readonly AppDbContext _context;

        public PersonController(IMapper mapper, IPersonService service, AppDbContext context)
        {
            _mapper = mapper;
            _service = service;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        
        {
            var result = _service.GetAllPeople();
            if (!result.Success)
            {
                TempData["ListError"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            ViewData["ListSuccess"] = result.Message;
            var mappedEntity = _mapper.Map<List<PersonListViewModel>>(result.Data);
            return View(mappedEntity);

        }

        [HttpGet]
        public IActionResult AddContact()
        {
            return View("AddContact");
        }
        [HttpPost]
        public IActionResult AddPerson(CreatePersonViewModel request)
        {
            if (ModelState.IsValid)
            {
                var mapToEntity = _mapper.Map<CreatePersonDto>(request);
                var result = _service.AddPerson(mapToEntity);
                if (!result.Success)
                {
                    TempData["CreateError"] = result.Message;
                    return RedirectToAction("Index");
                }
                TempData["CreateSuccess"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddContact", request);
            }


        }

        public IActionResult Remove(DeletePersonViewModel request,int id)
        {
            var mappedEntity = _mapper.Map<DeletePersonDto>(request);
            var deleteToEntity = _service.GetPerson(mappedEntity.Id);

            var data = deleteToEntity.Data;
            var entity = _mapper.Map<PersonDetailDto>(data);

            var result = _service.DeletePerson(entity);

            if (!result.Success)
            {
                TempData["DeleteError"] = result.Message;
                return RedirectToAction("Index");
            }
            TempData["DeleteSuccess"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var personToUpdate = _service.GetPerson(id);
            var data = personToUpdate.Data;

            var mapToEntity = _mapper.Map<UpdatePersonViewModel>(data);
            return View(mapToEntity);
        }

        [HttpPost]
        public IActionResult UpdatePerson(UpdatePersonViewModel request)
        {
            var mappedEntity = _mapper.Map<GetPersonByIdDto>(request);
            var updateToEntity = _service.GetPerson(mappedEntity.Id);

            var updateToData = updateToEntity.Data;
            var updatedData = mappedEntity;


            var updatedEntity = _mapper.Map(updatedData, updateToData);
            var entity = _mapper.Map<UpdatePersonDto>(updatedEntity);

            var result = _service.UpdatePerson(entity);
            if (!result.Success)
            {

                ViewData["UpdateError"] = result.Message;
                return View("Update");
            }
            TempData["UpdateSuccess"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetDetail(int id)
        {
            var item = _service.GetPersonDetail(id);
            var mappedItem = _mapper.Map<GetPersonWithDetailByIdViewModel>(item);
            return View(mappedItem);
        }
        [HttpGet]
        public IActionResult GetDetailForUpdate(int id)
        {
            var item = _service.GetPersonDetail(id);
            //var mappedItem = _mapper.Map<Person>(item);
            return Json(item);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        public IActionResult Example()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPeople()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var peopleData = (from person in _context.People where person.IsDeleted == false select person);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    peopleData = peopleData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    peopleData = peopleData.Where(m => m.Name.Contains(searchValue)
                                                || m.Surname.Contains(searchValue)
                                                || m.Address.Contains(searchValue)
                                                || m.Gender.Contains(searchValue)
                                                || m.GitHub.Contains(searchValue));
                }
                recordsTotal = peopleData.Count();
                var data = peopleData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
