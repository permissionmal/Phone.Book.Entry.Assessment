using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Domain.DTO;
using PhoneBook.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhonebookService _phonebookService;
        public PhoneBookController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValues"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ViewPhonebook")]
        public async Task<IActionResult> ViewPhonebook(string searchValues)
        {
            try
            {
                var response = await _phonebookService.GetPhonebook(searchValues);
                return Ok(response);
            }

            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEntry")]
        public async Task<IActionResult> AddEntry([FromBody] PhonebookDTO request)
        {
            try
            {
                var response = await _phonebookService.AddPhonebook(request);
                return Ok(response);
            }

            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
