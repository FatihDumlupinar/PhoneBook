using PhoneBook.Contact.Entities.Db;
using PhoneBook.Contact.Repositories.Contexts;
using PhoneBook.Contact.Repositories.DataAccess.EntityFramework;
using PhoneBook.Contact.Repositories.Interfaces;

namespace PhoneBook.Contact.Repositories.Methods
{
    class ContactInfoRepository : EfCoreEntityRepository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ContactDbContext dbContext) : base(dbContext)
        {
        }
    }
}
