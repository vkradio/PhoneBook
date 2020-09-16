using Ardalis.GuardClauses;
using DddInfrastructure;

namespace Domain.Entities
{
    public class Contact : Entity, IAggregateRoot
    {
        private Contact() { } // Required by EF Core

        public Contact(string name, string telephone)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(telephone, nameof(telephone));

            (Name, Telephone) = (name, telephone);
        }

        public string Name { get; private set; } = default!;

        public string Telephone { get; private set; } = default!;

        public void SetId(long id) => Id = id;
        
        public void ChangeName(string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Name = name;
        }

        public void ChangeTelephone(string telephone)
        {
            Guard.Against.NullOrEmpty(telephone, nameof(telephone));
            Telephone = telephone;
        }
    }
}
