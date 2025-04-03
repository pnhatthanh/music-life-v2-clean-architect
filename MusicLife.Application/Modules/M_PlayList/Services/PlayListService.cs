using MusicLife.Application.Modules.M_PlayList.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_PlayList.Services
{
    public class PlayListService : IPlayListService
    {
        public Task AddSongAsync(Guid idPlayList, Guid idSong, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PlayListDTO> CreatePlayListAsync(CreatePlayListDTO playListDTO, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PlayListDTO> DeletePlayListAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PlayListDTO> GetPlayListByIdAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<PlayListDTO>, int)> GetPlayListsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSongAsync(Guid idPlayList, Guid idSong, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PlayListDTO> UpdatePlayListAsync(Guid id, Guid userId, UpdatePlayListDTO playListDTO)
        {
            throw new NotImplementedException();
        }
    }
}
