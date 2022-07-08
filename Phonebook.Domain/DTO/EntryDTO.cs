using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PhoneBook.Domain.DTO
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EntryDTO
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        [DataMember(Name = "name")]
        public string PhoneNumber { get; set; }
    }
}
