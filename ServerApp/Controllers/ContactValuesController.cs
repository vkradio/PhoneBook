using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Models.BindingTargets;
using System.Collections.Generic;
using System.Linq;

namespace ServerApp.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactValuesController : Controller
    {
        readonly DataContext context;

        public ContactValuesController(DataContext ctx) => context = ctx;

        [HttpGet("{id}")]
        public Contact? GetContact(long id)
        {
            var contact = context
                .Contacts
                .FirstOrDefault(p => p.ContactId == id);
            return contact;
        }

        [HttpGet]
        public IEnumerable<Contact> GetContacts(string? filter)
        {
            IQueryable<Contact> query = context.Contacts;

            if (!string.IsNullOrWhiteSpace(filter))
            {
#pragma warning disable CA1307 // Specify StringComparison
                query = query.Where(c => c.Name.Contains(filter));
#pragma warning restore CA1307 // Specify StringComparison
            }

            return query.OrderBy(c => c.ContactId).AsEnumerable();
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactData contactData)
        {
            if (ModelState.IsValid)
            {
                Guard.Against.Null(contactData, nameof(contactData));

                var contact = contactData.GetContact();
                context.Add(contact);
                context.SaveChanges();
                return Ok(contact.ContactId);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceContact(long id, [FromBody] ContactData contactData)
        {
            if (ModelState.IsValid)
            {
                Guard.Against.Null(contactData, nameof(contactData));

                var contact = contactData.GetContact();
                contact.ContactId = id;
                context.Update(contact);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteContact(long id)
        {
            context.Remove(new Contact { ContactId = id });
            context.SaveChanges();
        }
    }
}
