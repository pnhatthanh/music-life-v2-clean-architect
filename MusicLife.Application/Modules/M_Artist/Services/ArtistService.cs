﻿using AutoMapper;
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

namespace MusicLife.Application.Modules.M_Artist.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ISongRepository _songRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public ArtistService(IArtistRepository artistRepository, ISongRepository songRepository, IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _artistRepository = artistRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _songRepository = songRepository;
        }

        public async Task<ArtistDTO> CreateArtistAsync(CreateArtistDTO artistDTO)
        {
            Artist artist=_mapper.Map<Artist>(artistDTO);
            artist.ImagePath = await _cloudinaryService.UploadFileImageAsync(artistDTO.Image!);
            _artistRepository.Add(artist);
            await _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task DeleteArtistAsync(Guid id)
        {
            var artist=await _artistRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException();
            _artistRepository.Delete(artist);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ArtistDTO> GetArtistByIdAsync(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException();
            return _mapper.Map<ArtistDTO>(artist);
        }

        public async Task<IEnumerable<ArtistDTO>> GetArtistByNameAsync(string name)
        {
            var artists =await _artistRepository.GetAllAsync(
                expressions: artist => artist.ArtistName.ToLower().Contains(name.ToLower())
                );
            return _mapper.Map<List<ArtistDTO>>(artists);
        }

        public async Task<(IEnumerable<ArtistDTO>, int)> GetArtistsAsync(int? page, int? pageSize)
        {
            var artistParams = new PaginationParam<Artist>
            {
                Page = page ?? 1,
                PageSize = pageSize ?? 15,
                OrderBy = artist => artist.OrderByDescending(a => a.Followers)
            };
            var (artists, totalArtist) = await _artistRepository.GetPaginationAsync(artistParams);
            return (_mapper.Map<IEnumerable<ArtistDTO>>(artists), totalArtist);
        }

        public async Task<ArtistDTO> UpdateArtistAsync(Guid id, UpdateArtistDTO artistDTO)
        {
            var artist =await _artistRepository.GetByIdAsync(id)
                ?? throw new NotFoundException();
            _mapper.Map(artistDTO, artist);
            if(artistDTO.Image != null)
            {
                await _cloudinaryService.DeleteImageFileAsync(artist.ImagePath);
                artist.ImagePath = await _cloudinaryService.UploadFileImageAsync(artistDTO.Image);
            }
            _artistRepository.Update(artist);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ArtistDTO>(artist);
        }

        public async Task<(IEnumerable<SongDTO>, int)> GetAllSongsAsync(int? page, int? pageSize, Guid artistId)
        {
            PaginationParam<Song> param = new PaginationParam<Song>
            {
                Page = page ?? 1,
                PageSize = pageSize ?? 15,
                OrderBy = song => song.OrderByDescending(x => x.ListenCount),
                Expression = song=>song.ArtistId== artistId,
                Includes = [song=> song.Artist!]
            };
            var (songs, total) = await _songRepository.GetPaginationAsync(param);
            return (_mapper.Map<IEnumerable<SongDTO>>(songs), total);
        }
    }
}
