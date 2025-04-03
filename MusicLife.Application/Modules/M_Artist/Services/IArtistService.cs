using MusicLife.Application.Modules.M_Artist.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Artist.Services
{
    public interface IArtistService
    {
        Task<(IEnumerable<ArtistDTO>,int)> GetArtistsAsync(int? page, int? pageSize);
        //Task<IEnumerable<SongResponse>> GetAllSongs(int page, int pageSize, Guid id);
        Task<IEnumerable<ArtistDTO>> GetArtistByNameAsync(string name);
        Task<ArtistDTO> GetArtistByIdAsync(Guid id);
        Task<ArtistDTO> CreateArtistAsync(CreateArtistDTO artistDTO);
        Task DeleteArtistAsync(Guid id);
        Task<ArtistDTO> UpdateArtistAsync(Guid id, CreateArtistDTO artistDTO);
    }
}
