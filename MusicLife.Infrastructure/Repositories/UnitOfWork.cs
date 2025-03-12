using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MusicLife.Application.IRepositories;
using MusicLife.Infrastructure;
using System;
using System.Threading.Tasks;


namespace MusicApi.Infracstructure.Repositories
{
    public class UnitOfWork(DataContext dataContext) : IUnitOfWork
    {
        private readonly DataContext _dataContext = dataContext 
            ?? throw new ArgumentNullException(nameof(dataContext));
        private IDbContextTransaction? _transaction;

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _dataContext.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress to commit.");
            }
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress to rollback.");
            }

            await _transaction.RollbackAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }

    }
}
