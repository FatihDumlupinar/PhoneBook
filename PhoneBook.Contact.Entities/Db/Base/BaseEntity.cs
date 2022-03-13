using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Contact.Entities.Db.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
