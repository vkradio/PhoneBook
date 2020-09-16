using Ardalis.GuardClauses;
using DataAccessService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebAPI
{
    public static class SeedData
    {
        public static void SeedDatabase(PhoneBookDbContext context)
        {
            Guard.Against.Null(context, nameof(context));

            context.Database.Migrate();

            if (context.Contacts.Any())
                return;

            context.Contacts.AddRange(
                new Contact("Vitaliy K.", "+79998885533"),
                new Contact("Mr. Smith", "+15551356745"),
                new Contact("Chuck Norris", "+8888888888"),
                new Contact("George Bush Jr.", "+11111111111")
            );

            context.SaveChanges();
        }
    }
}
