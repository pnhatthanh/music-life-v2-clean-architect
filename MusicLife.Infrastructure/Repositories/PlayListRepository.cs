using Microsoft.EntityFrameworkCore;
using MusicLife.Application.IRepositories;
using MusicLife.Domain.Entities;
using MusicLife.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Infracstructure.Repositories
{
    internal class PlayListRepository(DataContext context) 
        : BaseRepository<PlayList>(context), IPlayListRepository
    {
        public async Task<IEnumerable<Song>> GetSong(Guid playlistId)
        {
            var songs =await _dbSet.Where(p => p.PlayListId == playlistId)
                        .SelectMany(p => p.Songs).Include(s => s.artist).ToListAsync();
            return songs;
        }
    }
}
