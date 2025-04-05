using AutoMapper;
using MusicLife.Application.Exceptions;
using MusicLife.Application.ExternalServices;
using MusicLife.Application.IRepositories;
using MusicLife.Application.Modules.M_Artist.DTOs;
using MusicLife.Application.Modules.M_Song.DTOs;
using MusicLife.Application.Params;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicLife.Application.Modules.M_Song.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserFavouriteRepository _userFavouriteRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SongService(ISongRepository songRepository, IArtistRepository artistRepository, ICategoryRepository categoryRepository,IUserFavouriteRepository userFavouriteRepository,
                            ICloudinaryService cloudinaryService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _categoryRepository = categoryRepository;
            _userFavouriteRepository = userFavouriteRepository;
            _cloudinaryService = cloudinaryService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SongDTO> CreatSongAsync(CreateSongDTO songDTO)
        {
            if (!await _categoryRepository.ExistAsync(category => category.CategoryId == songDTO.CategoryId))
                throw new NotFoundException("Category not found");
            if (!await _artistRepository.ExistAsync(artist => artist.ArtistId == songDTO.ArtistId))
                throw new NotFoundException("Artist not found");
            Song song = _mapper.Map<Song>(songDTO);
            song.SongImagePath = await _cloudinaryService.UploadFileImageAsync(songDTO.ImageFile!);
            song.SongPath = await _cloudinaryService.UploadFileAudioAsync(songDTO.AudioFile!);
            _songRepository.Add(song);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SongDTO>(song);
        }

        public async Task DeleteSongAsync(Guid id)
        {
            Song song=await _songRepository.GetByIdAsync(id) ?? throw new NotFoundException();
            _songRepository.Delete(song);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SongDTO>> GetRecentlyPlayAsync(Guid[] idSongs)
        {
            if (idSongs == null || idSongs.Length == 0)
                return Enumerable.Empty<SongDTO>();
            var idSet = new HashSet<Guid>(idSongs);
            var songs =await _songRepository.GetAllAsync(
                    expressions: song => idSet.Contains(song.SongId),
                    includes: song => song.Artist!);
            var sortedSong=songs.OrderBy(song=>Array.IndexOf(idSongs, song.SongId)).ToList(); 
            return _mapper.Map<IEnumerable<SongDTO>>(sortedSong);
        }

        public async Task<SongDTO> GetSongByIdAsync(Guid id, Guid? userId)
        {
            var song =await _songRepository.FirstOrDefaultAsync(
                    expressions: song=>song.SongId == id,
                    includes: song=> song.Artist!) ?? throw new NotFoundException();
            song.ListenCount++;
            _songRepository.Update(song);
            var songResponse= _mapper.Map<SongDTO>(song);
            if (userId != null)
            {
                songResponse.IsFavourite = await _userFavouriteRepository.ExistAsync(us => us.UserId == userId && us.SongId == id);
            }
            await _unitOfWork.SaveChangesAsync();
            return songResponse;
        }

        public async Task<IEnumerable<SongDTO>> GetSongByNameAsync(string title)
        {
            var songs = await _songRepository.GetAllAsync(
                expressions: song => song.SongName.ToLower().Contains(title.ToLower()),
                includes: song=>song.Artist!
                );
            return _mapper.Map<IEnumerable<SongDTO>>(songs);
        }

        public async Task<(IEnumerable<SongDTO>, int)> GetSongsAsync(int? page, int? pageSize)
        {
            var parameter = new PaginationParam<Song>
            {
                Page = page ?? 1,
                PageSize = pageSize ?? 15,
                OrderBy = artist => artist.OrderByDescending(s => s.ListenCount)
            };
            var (songs, totalSongs) = await _songRepository.GetPaginationAsync(parameter);
            return (_mapper.Map<IEnumerable<SongDTO>>(songs), totalSongs);
        }

        public async Task<SongDTO> UpdateSongAsync(Guid id, UpdateSongDTO songDTO)
        {
            var song = await _songRepository.GetByIdAsync(id)
                ?? throw new NotFoundException();
            _mapper.Map(songDTO, song);
            if (songDTO.ImageFile != null)
            {
                await _cloudinaryService.DeleteImageFileAsync(song.SongImagePath);
                song.SongImagePath = await _cloudinaryService.UploadFileImageAsync(songDTO.ImageFile);
            }
            if (songDTO.AudioFile != null)
            {
                await _cloudinaryService.DeleteAudioFileAsync(song.SongPath);
                song.SongPath = await _cloudinaryService.UploadFileAudioAsync(songDTO.AudioFile);
            }
            _songRepository.Update(song);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SongDTO>(song);
        }
    }
}
