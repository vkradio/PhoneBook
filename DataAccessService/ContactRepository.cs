using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessService
{
    public class ContactRepository : IContactRepository
    {
        readonly PhoneBookDbContext dbContext;

        public ContactRepository(PhoneBookDbContext dbContext) => this.dbContext = dbContext;

        public async Task<IEnumerable<Contact>> GetContactsAsync(string? filter)
        {
            IQueryable<Contact> query = dbContext.Contacts;

            if (!string.IsNullOrWhiteSpace(filter))
            {
#pragma warning disable CA1307 // Specify StringComparison
                query = query.Where(c => c.Name.Contains(filter));
#pragma warning restore CA1307 // Specify StringComparison
            }

            return await query.OrderBy(c => c.Id).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Contact?> GetContactByIdAsync(long contactId) =>
            await dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == contactId).ConfigureAwait(false);

        public void InsertContact(Contact contact) => dbContext.Contacts.Add(contact);

        public async Task DeleteContactAsync(long contactId)
        {
            var contact = await GetContactByIdAsync(contactId).ConfigureAwait(false);
            if (contact != null)
                dbContext.Contacts.Remove(contact);
        }

        public void UpdateContact(Contact contact) =>
            dbContext.Entry(contact).State = EntityState.Modified;

        public async Task SaveAsync(CancellationToken cancellationToken = default) =>
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
