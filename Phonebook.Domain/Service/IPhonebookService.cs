using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Domain.DTO;

namespace PhoneBook.Domain.Service
{
    public interface IPhonebookService
    {
        Task<PhonebookDTO> AddPhonebook(PhonebookDTO request);
        Task<PhonebookDTO> GetPhonebook(string phoneNumber);
    }
}
