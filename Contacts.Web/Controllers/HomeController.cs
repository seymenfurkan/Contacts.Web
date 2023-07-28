using Contacts.Business.Abstract;
using Contacts.Entities.Concrete;
using Contacts.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contacts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonService _personService;

        public HomeController(ILogger<HomeController> logger, IPersonService personService = null)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index(int pg=1)
        
        {
            //Client-Side Pagination
            //var people = _personService.GetAllPeople();

            //const int pageSize = 1;
            //if (pg < 1) pg = 1;

            //int recsCount = people.Data.Count;

            //var pager = new Pager(recsCount, pg, pageSize);
            //int recSkip = (pg - 1) * pageSize;
            //var data = people.Data.Skip(recSkip).Take(pager.PageSize).ToList();

            //this.ViewBag.Pager = pager;
        
            //data
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}