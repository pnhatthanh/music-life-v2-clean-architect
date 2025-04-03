using MusicLife.Application.Modules.M_PlayList.DTOs;
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
        Task<(IEnumerable<PlayListDTO>, int)> GetPlayListsAsync(Guid userId);
        Task<PlayListDTO> GetPlayListByIdAsync(Guid id, Guid userId);
        Task<PlayListDTO> CreatePlayListAsync(CreatePlayListDTO playListDTO, Guid userId);
        Task<PlayListDTO> DeletePlayListAsync(Guid id, Guid userId);
        Task<PlayListDTO> UpdatePlayListAsync(Guid id, Guid userId, UpdatePlayListDTO playListDTO);
        //Task<IEnumerable<SongResponse>> GetSongs(Guid playlistId);
        Task AddSongAsync(Guid idPlayList, Guid idSong, Guid userId);
        Task RemoveSongAsync(Guid idPlayList, Guid idSong, Guid userId);
    }
}
