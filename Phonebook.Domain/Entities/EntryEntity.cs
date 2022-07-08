using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneBook.Domain.Entities
{
    [Table("Entry", Schema = "dbo")]
    public class EntryEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
