using Contacts.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Entities.DTOs
{
    public class DeletePersonDto : IDto
    {
        public int Id { get; set; }
    }
}
