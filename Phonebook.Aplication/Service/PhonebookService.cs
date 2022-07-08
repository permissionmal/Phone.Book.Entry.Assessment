using AutoMapper;
using PhoneBook.Domain.DTO;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Repositories;
using PhoneBook.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Application.Service
{
    public class PhonebookService : IPhonebookService
    {
        private readonly IPhonebookRepository _phonebookRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public PhonebookService(IPhonebookRepository phonebookRepository, IEntryRepository entryRepository, IMapper mapper)
        {
            _phonebookRepository = phonebookRepository;
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<PhonebookDTO> AddPhonebook(PhonebookDTO request)
        {
            var phonebookDetail = new PhonebookEntity
            {
                Name = request.Name
            };
            var phonebookEntity = await _phonebookRepository.AddAsync(phonebookDetail);

            foreach (var entryData in request.Entries)
            {
                var entryDetail = new EntryEntity
                {
                    Name = entryData.Name,
                    PhoneNumber = entryData.PhoneNumber                   
                };
                var entryEntity = await _entryRepository.AddAsync(entryDetail);
            }
            return request;
         }

        public async Task<PhonebookDTO> GetPhonebook(string searchValue)
        {
            //use this to search phonenumber or name
            var persistedData = await _entryRepository.GetAllAsync(x => x.PhoneNumber.Contains(searchValue) || x.Name.Contains(searchValue));

            var result = new PhonebookDTO();
            var detailsMap = new List<PhonebookDTO>();
            if (persistedData.Count > 0)
            {
                var entryDetail = persistedData.Select(x => x).FirstOrDefault();
                var phonebookDetails = await _phonebookRepository.GetAllAsync(x => x.Name.Contains(entryDetail.Name));

                foreach (var details in phonebookDetails)
                {
                    var phonebookDetail = new PhonebookEntity
                    {
                        Name = details.Name
                    };

                    var entryDetails = await _entryRepository.GetAllAsync(x => x.PhoneNumber.Contains(entryDetail.PhoneNumber) || x.Name.Contains(entryDetail.Name));
                    var entryDetailsMap = _mapper.Map<List<EntryDTO>>(entryDetails);

                    result = _mapper.Map<PhonebookDTO>(details);
                    result.Entries = entryDetailsMap;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
