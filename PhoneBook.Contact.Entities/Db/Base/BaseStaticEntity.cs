using System;

namespace PhoneBook.Contact.Entities.Db.Base
{
    public abstract class BaseStaticEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
