namespace ServerApp.Models
{
    public class Contact
    {
        public long ContactId { get; set; }

        public string Name { get; set; } = default!;

        public string Telephone { get; set; } = default!;
    }
}
