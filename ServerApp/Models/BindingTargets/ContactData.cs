using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class ContactData
    {
        Contact contact = new Contact();

        [Required]
        public string? Name { get => contact.Name; set => contact.Name = value ?? string.Empty; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? Telephone { get => contact.Telephone; set => contact.Telephone = value ?? string.Empty; }

        public Contact GetContact() => new Contact
        {
            Name = Name!,
            Telephone = Telephone!
        };

        public void SetContact(Contact contact) => this.contact = contact;
    }
}
