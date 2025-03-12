using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.IRepositories
{
    public interface ISongRepository : IBaseRepository<Song>
    {
        public new Task<IEnumerable<Song>> GetAllPaged(int? page, int? pageSize, params Expression<Func<Song, object>>[] includes);
    }
}
