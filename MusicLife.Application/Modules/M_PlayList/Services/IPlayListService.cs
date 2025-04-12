using MusicLife.Application.Modules.M_PlayList.DTOs;
using MusicLife.Application.Modules.M_Song.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_PlayList.Services
{
    public interface IPlayListService
    {
        Task<IEnumerable<PlayListDTO>> GetPlayListsAsync();
        Task<PlayListDTO> GetPlayListByIdAsync(Guid id);
        Task<PlayListDTO> CreatePlayListAsync(CreatePlayListDTO playListDTO);
        Task DeletePlayListAsync(Guid id);
        Task<PlayListDTO> UpdatePlayListAsync(Guid id, UpdatePlayListDTO playListDTO);
        Task<IEnumerable<SongDTO>> GetSongsAsync(Guid playlistId);
        Task AddSongAsync(Guid idPlayList, Guid idSong);
        Task RemoveSongAsync(Guid idPlayList, Guid idSong);
    }
}
