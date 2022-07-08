using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneBook.Domain.Entities
{
    [Table("Phonebook", Schema = "dbo")]
    public class PhonebookEntity
    {
        public string Name { get; set; }
        public virtual EntryEntity Entries { get; set; }
    }
}
