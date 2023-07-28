using Contacts.Core.DataAccessRepositories.EntityFramework;
using Contacts.DataAccess.Abstract;
using Contacts.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfPersonDal : EfEntityRepositoryBase<Person, AppDbContext>, IPersonDal
    {
    }
}
