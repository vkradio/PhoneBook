using DddInfrastructure;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IContactRepository : IAsyncRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetContactsAsync(string? filter);

        Task<Contact?> GetContactByIdAsync(long contactId);

        void InsertContact(Contact contact);

        Task DeleteContactAsync(long contactId);

        void UpdateContact(Contact contact);

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
