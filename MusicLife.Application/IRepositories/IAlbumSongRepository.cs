using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.IRepositories
{
    public interface IAlbumSongRepository : IBaseRepository<AlbumSong>
    {
        Task<IEnumerable<Song?>> GetSongs(Guid idAlbum);
    }
}
