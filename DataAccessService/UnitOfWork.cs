using DddInfrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessService
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly PhoneBookDbContext dbContext;

        public UnitOfWork(PhoneBookDbContext phoneBookDbContext)
        {
            dbContext = phoneBookDbContext;
            ContactRepository = new ContactRepository(dbContext);
        }

        public ContactRepository ContactRepository { get; private set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        #region Dispose
        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
