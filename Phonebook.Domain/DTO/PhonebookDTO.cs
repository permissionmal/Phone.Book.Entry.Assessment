using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhoneBook.Domain.DTO
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PhonebookDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public PhonebookDTO()
        {
            Entries = new List<EntryDTO>();
        }
        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// List of Entries
        /// </summary>
        [DataMember(Name = "entries")]
        public List<EntryDTO> Entries { get; set; }
    }
}
