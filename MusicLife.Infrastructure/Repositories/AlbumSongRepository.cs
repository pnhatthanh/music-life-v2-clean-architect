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
        : BaseRepository<AlbumSong>(context), IAlbumSongRepository
    {
        public async Task<IEnumerable<Song>> GetSongsOfAlbum(Guid idAlbum)
        {
            var songs = await _dbSet.Include(albumSong => albumSong.Song)
                .ThenInclude(song => song!.Artist)
                .Where(albumSong => albumSong.AlbumId == idAlbum).Select(albumSong => albumSong.Song).ToListAsync();
            return songs;
        }
    }
}
