using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.IRepositories
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task<int> SaveChangesAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
