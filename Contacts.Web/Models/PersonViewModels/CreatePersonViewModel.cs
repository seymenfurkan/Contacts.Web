using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Contacts.Core.Entities.Abstract;
using System.Diagnostics.Metrics;

namespace Contacts.Web.Models.PersonViewModels
{
    public class CreatePersonViewModel : IViewModel
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz !")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string GitHub { get; set; }
    }
}
