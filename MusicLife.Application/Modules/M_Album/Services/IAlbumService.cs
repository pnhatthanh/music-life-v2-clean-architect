﻿using MusicLife.Application.Modules.M_Album.DTOs;
using MusicLife.Application.Modules.M_Song.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Album.Services
{
    public interface IAlbumService
    {
        Task<(IEnumerable<AlbumDTO>, int)> GetAlbumsAsync(int? page, int? pageSize);
        Task<AlbumDTO> GetAlbumByIdAsync(Guid id);
        Task<AlbumDTO> CreatAlbumAsync(CreateAlbumDTO albumDTO);
        Task DeleteAlbumAsync(Guid id);
        Task<AlbumDTO> UpdateAlbumAsync(Guid id, UpdateAlbumDTO album);
        Task<IEnumerable<SongDTO>> GetAllSongOfAlbumAsync(Guid albumId);
        Task AddSongToAlbumAsync(Guid idAlbum, Guid idSong);
        Task<bool> IsExist(Guid idAlbum);
    }
}
