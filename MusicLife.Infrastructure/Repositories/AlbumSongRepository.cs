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
    public class AlbumSongRepository(DataContext context)
        : BaseRepository<DataContext>(context), IAlbumSongRepository
    {
        public async Task<IEnumerable<Song?>> GetSongs(Guid idAlbum)
        {
            var songs=await _dbSet.Include(a=>a.Song).ThenInclude(s=>s!.artist)
                .Where(@as=>@as.AlbumId==idAlbum).Select(a=>a.Song).ToListAsync();
            return songs;
        }
    }
}
