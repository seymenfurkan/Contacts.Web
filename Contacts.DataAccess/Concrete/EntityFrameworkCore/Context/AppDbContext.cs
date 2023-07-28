using Contacts.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Contacts.DataAccess.Concrete.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-DIER1P5\MSSQLSERVER02;Initial Catalog=ContactsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Person> People { get; set; }
        
    }
}
