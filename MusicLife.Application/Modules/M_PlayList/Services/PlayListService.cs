﻿using AutoMapper;
using MusicLife.Application.Exceptions;
using MusicLife.Application.IRepositories;
using MusicLife.Application.Modules.CurrentUser;
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
    public class PlayListService : IPlayListService
    {
        private readonly IPlayListRepository _playListRepository;
        private readonly ISongRepository _songRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public PlayListService(IPlayListRepository playListRepository,IUserRepository userRepository ,IUnitOfWork unitOfWork, IMapper mapper, ISongRepository songRepository, ICurrentUserService currentUserService)
        {
            _playListRepository = playListRepository;
            _songRepository = songRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task AddSongAsync(Guid idPlayList, Guid idSong)
        {
            var userId = _currentUserService.UserId;
            var song = await _songRepository.GetByIdAsync(idSong)
                        ?? throw new NotFoundException();
            var playList = await _playListRepository.FirstOrDefaultAsync(
                        expressions: playList => playList.PlayListId == idPlayList,
                        includes: playList => playList.Songs)?? throw new NotFoundException();
            if (playList.UserId != userId)
                throw new ForbiddenException();
            if (playList.Songs.Contains(song))
                throw new DuplicatedException("Song already added to playlist");
            playList.Songs.ToList().Add(song);
            playList.NumberOfSong++;
            _playListRepository.Update(playList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PlayListDTO> CreatePlayListAsync(CreatePlayListDTO playListDTO)
        {
            var userId = _currentUserService.UserId;
            var user = await _userRepository.GetByIdAsync(userId)
                        ?? throw new NotFoundException("User not found");
            if (await _playListRepository.ExistAsync(p => p.PlayListName == playListDTO.PlayListName && p.UserId == userId))
                throw new DuplicatedException("Name's playlist already exists");
            PlayList playList = _mapper.Map<PlayList>(playListDTO);
            playList.UserId = userId;
            _playListRepository.Add(playList);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PlayListDTO>(playList);
        }

        public async Task DeletePlayListAsync(Guid id)
        {
            var userId = _currentUserService.UserId;
            PlayList playList = await _playListRepository.FirstOrDefaultAsync(playList => playList.PlayListId == id && playList.UserId == userId)
                        ?? throw new NotFoundException();
            _playListRepository.Delete(playList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PlayListDTO> GetPlayListByIdAsync(Guid id)
        {
            var userId = _currentUserService.UserId;
            PlayList playList = await _playListRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException();
            if (playList.UserId != userId)
                throw new ForbiddenException();
            return _mapper.Map<PlayListDTO>(playList);
        }

        public async Task<IEnumerable<PlayListDTO>> GetPlayListsAsync()
        {
            var userId = _currentUserService.UserId;
            var playList = await _playListRepository.GetAllAsync(playList => playList.UserId == userId);
            return _mapper.Map<IEnumerable<PlayListDTO>>(playList);
        }

        public async Task<IEnumerable<SongDTO>> GetSongsAsync(Guid playlistId)
        {
            var userId = _currentUserService.UserId;
            var playlist =await _playListRepository.FirstOrDefaultAsync(
                                expressions: playList => playList.PlayListId == playlistId &&  playList.UserId==userId,
                                includes: playList => playList.Songs
                            ) ??  throw new NotFoundException();
            return _mapper.Map<IEnumerable<SongDTO>>(playlist.Songs);
        }

        public async Task RemoveSongAsync(Guid idPlayList, Guid idSong)
        {
            var userId = _currentUserService.UserId;
            var song=await _songRepository.GetByIdAsync(idSong)
                       ?? throw new NotFoundException();
            var playList = await _playListRepository.FirstOrDefaultAsync(
                       expressions: playList => playList.PlayListId == idPlayList && playList.UserId==userId,
                       includes: playList => playList.Songs)?? throw new NotFoundException();
            if (!playList.Songs.Contains(song))
                throw new BadRequestException();
            playList.Songs.ToList().Remove(song);
            _playListRepository.Update(playList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PlayListDTO> UpdatePlayListAsync(Guid id, UpdatePlayListDTO playListDTO)
        {
            var userId = _currentUserService.UserId;
            var playList = await _playListRepository.FirstOrDefaultAsync(
                        expressions: playList => playList.PlayListId == id && playList.UserId == userId,
                        includes: playList => playList.Songs) ?? throw new NotFoundException();
            _mapper.Map(playListDTO, playList);
            _playListRepository.Update(playList);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PlayListDTO>(playList);
        }
    }
}
