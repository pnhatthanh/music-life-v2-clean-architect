using AutoMapper;
using MusicLife.Application.Dtos;
using MusicLife.Application.Exceptions;
using MusicLife.Application.ExternalServices;
using MusicLife.Application.IRepositories;
using MusicLife.Application.Modules.M_Album.DTOs;
using MusicLife.Application.Params;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Album.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;
        private readonly IAlbumSongRepository _albumSongRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public AlbumService(IAlbumRepository albumRepository, ISongRepository songRepository, IAlbumSongRepository albumSongRepository, IMapper mapper, ICloudinaryService cloudinaryService, IUnitOfWork unitOfWork)
        {
            _albumRepository = albumRepository;
            _songRepository = songRepository;
            _albumSongRepository = albumSongRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task AddSongToAlbumAsync(Guid idAlbum, Guid idSong)
        {
            Album album = await _albumRepository.FirstOrDefaultAsync(album => album.AlbumId == idAlbum)
                ?? throw new NotFoundException($"Album with ID {idAlbum} not found");
            Song song = await _songRepository.GetByIdAsync(idSong)
                ?? throw new NotFoundException($"Song with ID{idSong} not found");
            if (await _albumSongRepository.ExistAsync(albumSong=>albumSong.SongId==song.SongId && albumSong.AlbumId==album.AlbumId))
                throw new DuplicatedException("Song already added to album");
            _albumSongRepository.Add(new AlbumSong
            {
                AlbumId = album.AlbumId,
                SongId = song.SongId,
            });
            album.NumberOfSong++;
            _albumRepository.Update(album);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AlbumDTO> CreatAlbumAsync(CreateAlbumDTO albumDTO)
        {
            Album album = _mapper.Map<Album>(albumDTO);
            album.ImagePath = await _cloudinaryService.UploadFileImageAsync(albumDTO.ImageFile!);
            albumDTO.SongIDs.ForEach(async songID =>
            {
                Song song = await _songRepository.GetByIdAsync(songID)
                    ?? throw new NotFoundException($"Song with ID {songID} not found");
                var albumSong = new AlbumSong
                {
                    AlbumId = album.AlbumId,
                    SongId = song.SongId
                };
                _albumSongRepository.Add(albumSong);
            });
            album.NumberOfSong = albumDTO.SongIDs.Count;
            _albumRepository.Add(album);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AlbumDTO>(album);
        }

        public async Task DeleteAlbumAsync(Guid id)
        {
            var album = await _albumRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Not found");
            _albumRepository.Delete(album);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AlbumDTO> GetAlbumByIdAsync(Guid id)
        {
            var album = await _albumRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Not found");
            return _mapper.Map<AlbumDTO>(album);
        }

        public async Task<(IEnumerable<AlbumDTO>,int)> GetAlbumsAsync(int? page, int? pageSize)
        {
            var albumParams = new PaginationParam<Album>
            {
                Page = page ?? 1,
                PageSize = pageSize ?? 15,
                OrderBy = album => album.OrderByDescending(album => album.CreatedTime),
            };
            var (albums, totalItem) = await _albumRepository.GetPaginationAsync(albumParams);
           return (_mapper.Map<IEnumerable<AlbumDTO>>(albums), totalItem);
        }

        public async Task<IEnumerable<SongResponse>> GetAllSongOfAlbumAsync(Guid id)
        {
            var songs =await _albumSongRepository.GetSongsOfAlbum(id)
                    ?? throw new NotFoundException("Not found");
            return _mapper.Map<IEnumerable<SongResponse>>(songs); 
        }

        public async Task<bool> IsExist(Guid idAlbum)
        {
            return await _albumRepository.ExistAsync(album=>album.AlbumId == idAlbum);
        }

        public async Task<AlbumDTO> UpdateAlbumAsync(Guid id, UpdateAlbumDTO albumDTO)
        {
            var album = await _albumRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Not found album");
            _mapper.Map(albumDTO, album);
            _albumRepository.Update(album);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AlbumDTO>(album);
        }
    }
}
