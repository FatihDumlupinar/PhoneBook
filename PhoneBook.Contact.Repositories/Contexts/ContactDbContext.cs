using Microsoft.EntityFrameworkCore;
using PhoneBook.Contact.Entities.Db;
using System.Diagnostics.CodeAnalysis;

namespace PhoneBook.Contact.Repositories.Contexts
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ContactInfo> ContactInfos { get; set; }
        public virtual DbSet<ContactDetail> ContactDetails { get; set; }
        public virtual DbSet<StaticContactType> StaticContactTypes { get; set; }

    }
}
