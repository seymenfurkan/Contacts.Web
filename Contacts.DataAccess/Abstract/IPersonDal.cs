using Contacts.Core.DataAccessRepositories;
using Contacts.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.DataAccess.Abstract
{
    public interface IPersonDal : IRepository<Person>
    {

    }
}
