using DddInfrastructure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessService
{
    public class PhoneBookDbContext : DbContext, IUnitOfWork
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> opts)
            : base(opts) { }

        public DbSet<Contact> Contacts { get; set; } = default!;
    }
}
