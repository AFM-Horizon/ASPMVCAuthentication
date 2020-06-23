using System.Data.Entity;
using ASPMVCAuthentication.Models;

namespace ASPMVCAuthentication.Data
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}