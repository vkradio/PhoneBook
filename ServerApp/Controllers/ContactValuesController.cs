using Ardalis.GuardClauses;
using DataAccessService;
using DddInfrastructure;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactValuesController : Controller
    {
        readonly UnitOfWork unitOfWork;

        public ContactValuesController(IUnitOfWork uow) => unitOfWork = (UnitOfWork)uow;

        [HttpGet("{id}")]
        public async Task<Contact?> GetContact(long id) =>
            await unitOfWork.ContactRepository.GetContactByIdAsync(id).ConfigureAwait(true);

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetContacts(string? filter) =>
            await unitOfWork.ContactRepository.GetContactsAsync(filter).ConfigureAwait(true);

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Guard.Against.Null(viewModel, nameof(viewModel));

                var contact = viewModel.GetContact();
                unitOfWork.ContactRepository.InsertContact(contact);
                await unitOfWork.SaveChangesAsync().ConfigureAwait(true);
                return Ok(contact.Id);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceContact(long id, [FromBody] ContactViewModel contactData)
        {
            if (ModelState.IsValid)
            {
                Guard.Against.Null(contactData, nameof(contactData));

                var contact = contactData.GetContact();
                contact.SetId(id);
                unitOfWork.ContactRepository.UpdateContact(contact);
                await unitOfWork.SaveChangesAsync().ConfigureAwait(true);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteContact(long id)
        {
            await unitOfWork.ContactRepository.DeleteContactAsync(id).ConfigureAwait(true);
            await unitOfWork.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
