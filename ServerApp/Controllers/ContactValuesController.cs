﻿using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using ServerApp.Models.BindingTargets;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactValuesController : Controller
    {
        readonly DataContext context;

        public ContactValuesController(DataContext ctx) => context = ctx;

        [HttpGet]
        public IEnumerable<Contact> GetContacts() => context.Contacts.AsNoTracking();

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
