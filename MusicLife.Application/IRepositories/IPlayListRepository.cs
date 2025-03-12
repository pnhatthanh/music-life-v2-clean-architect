using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.IRepositories
{
    public interface IPlayListRepository : IBaseRepository<PlayList>
    {
        public Task<IEnumerable<Song>> GetSong(Guid playlistId);
    }
}
