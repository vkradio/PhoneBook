﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace DddInfrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
