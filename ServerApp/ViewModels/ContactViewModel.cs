using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class ContactViewModel
    {
        Contact contact = new Contact("dummy", "dummy");

        [Required]
        public string? Name
        {
            get => contact.Name;

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    contact.ChangeName(value);
            }
        }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? Telephone
        {
            get => contact.Telephone;

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    contact.ChangeTelephone(value);
            }
        }

        public Contact GetContact() => new Contact(Name!, Telephone!);

        public void SetContact(Contact contact) => this.contact = contact;
    }
}
