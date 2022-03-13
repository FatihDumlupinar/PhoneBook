using PhoneBook.Contact.Entities.Db;
using PhoneBook.Contact.Repositories.DataAccess;

namespace PhoneBook.Contact.Repositories.Interfaces
{
    public interface IContactInfoRepository : IEntityRepository<ContactInfo>
    {
    }
}
