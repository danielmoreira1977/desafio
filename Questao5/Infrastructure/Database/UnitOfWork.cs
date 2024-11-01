using Questao5.Domain.Repositories;

namespace Questao5.Infrastructure.Database
{

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbSession _session;

        public UnitOfWork(IDbSession session)
        {
            _session = session;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }



}
