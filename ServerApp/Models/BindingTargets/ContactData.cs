using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class ContactData
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? Telephone { get; set; }

        public Contact GetContact() => new Contact
        {
            Name = Name!,
            Telephone = Telephone!
        };
    }
}
