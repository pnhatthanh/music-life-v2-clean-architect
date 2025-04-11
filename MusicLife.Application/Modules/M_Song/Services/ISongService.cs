﻿using MusicLife.Application.Modules.M_Song.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Song.Services
{
    public interface ISongService
    {
        Task<(IEnumerable<SongDTO>, int)> GetSongsAsync(int? page, int? pageSize);
        Task<IEnumerable<SongDTO>> GetRecentlyPlayAsync(Guid[] idSongs);
        Task<SongDTO> GetSongByIdAsync(Guid id, Guid? userId);
        Task<SongDTO> CreatSongAsync(CreateSongDTO songDTO);
        Task DeleteSongAsync(Guid id);
        Task<SongDTO> UpdateSongAsync(Guid id, UpdateSongDTO song);
        Task<IEnumerable<SongDTO>> GetSongByNameAsync(string title);

        //Favourite songs
        Task<SongDTO> AddSongToFavouritesAsync(Guid idSong, Guid userId);
        Task<(IEnumerable<SongDTO>, int)> GetFavouriteSongsAsync(Guid userId, int? page, int? pageSize);
        Task RemoveSongFavouriteAsync(Guid idSong, Guid userId);

    }
}
