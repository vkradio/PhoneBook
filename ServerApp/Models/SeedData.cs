using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ServerApp.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            Guard.Against.Null(context, nameof(context));

            context.Database.Migrate();

            if (context.Contacts.Any())
                return;

            context.Contacts.AddRange(
                new Contact { Name = "Vitaliy K.", Telephone = "+79998885533" },
                new Contact { Name = "Mr. Smith", Telephone = "+15551356745" },
                new Contact { Name = "Chuck Norris", Telephone = "+8888888888" },
                new Contact { Name = "George Bush Jr.", Telephone = "+11111111111" }
            );

            context.SaveChanges();
        }
    }
}
