//using AutoMapper;
//using MusicLife.Application.Dtos;
//using MusicLife.Application.IRepositories;
//using MusicLife.Domain.Entities;

//namespace MusicLife.Application.Services.AlbumService
//{
//    public class AlbumService : IAlbumService
//    {
//        public readonly IAlbumRepository _albumRepository;
//        public readonly ISongRepository _songRepository;
//        public readonly IAlbumSongRepository _albumSongRepository;
//        public readonly IMapper _mapper;
//        public AlbumService(IAlbumRepository albumRepository, ISongRepository songRepository,
//            IAlbumSongRepository albumSongRepository,IMapper mapper)
//        {
//            _albumRepository = albumRepository;
//            _songRepository = songRepository;
//            _albumSongRepository = albumSongRepository;
//            _mapper = mapper;
//        }

//        public async Task<Album> CreatAlbum(AlbumDTO albumDTO)
//        {
//            Album album = _mapper.Map<Album>(albumDTO);
//            //album.ImagePath = await _fileHelper.UploadFileImage(albumDTO.ImageFile);
//            albumDTO.SongIDs.ForEach(songID =>
//            {
//                Song song =_songRepository.GetById(songID)
//                    ?? throw new Exception($"Song with ID {songID} not found");
//                var albumSong = new AlbumSong
//                {
//                    AlbumId = album.AlbumId,
//                    SongId = song.SongId
//                };
//                album.AlbumSongs.Add(albumSong);
//            });
//            album.NumberOfSong = albumDTO.SongIDs.Count;
//            await _albumRepository.AddAsynch(album);
//            return album;
//        }
//        public async Task AddSongToAlbum(Guid idAlbum, Guid idSong)
//        {
//            Album album=await _albumRepository.FirstOrDefaultWithIncludes(album=>album.AlbumId==idAlbum,album=>album.AlbumSongs)
//                ?? throw new Exception($"Album with ID {idAlbum} not found");
//            Song song = await _songRepository.GetByIdAsynch(idSong)
//                ?? throw new Exception($"Song with ID{idSong} not found");
//            if(album.AlbumSongs.Any(a=>a.SongId==song.SongId))
//            {
//                throw new Exception("Song already added to album");
//            }
//            album.AlbumSongs.Add(new AlbumSong
//            {
//                AlbumId=album.AlbumId,
//                SongId=song.SongId
//            });
//            album.NumberOfSong++;
//            await _albumRepository.UpdateAsynch(album);
//        }
//        public async Task<Album> DeleteAlbum(Guid id)
//        {
//            var album = await _albumRepository.GetByIdAsynch(id) 
//                ?? throw new Exception("Not found");
//            await _albumRepository.Delete(album);
//            return album;
//        }

//        public async Task<Album> GetAlbumById(Guid id)
//        {
//            var album = await _albumRepository.GetByIdAsynch(id)
//                ?? throw new Exception("Not found");
//            return album;
//        }
//        public async Task<IEnumerable<SongResponse>> GetAllSongOfAlbum(Guid id)
//        {
//            var album = await _albumSongRepository.GetSongs(id)
//                      ?? throw new Exception("Album not found");
//            return _mapper.Map<IEnumerable<SongResponse>>(album);
//        }
//        public async Task<IEnumerable<Album>> GetAllAlbums()
//        {
//            return await _albumRepository.GetAll();
//        }
//        public async Task<IEnumerable<Album>> GetAllAlbumWithPaged(int? page, int? pageSize)
//        {
//            if (page < 1) page = 1;
//            return await _albumRepository.GetAllPaged(page, pageSize);
//        }

//        public async Task<Album> UpdateAlbum(Guid id, AlbumDTO albumDTO)
//        {
//            var album = await _albumRepository.GetByIdAsynch(id) 
//                ?? throw new ArgumentException("Not found album");
//            _mapper.Map(albumDTO, album);
//            await _albumRepository.UpdateAsynch(album);
//            return album;
//        }

//        public async Task<bool> IsExist(Guid idAlbum)
//        {
//            return await _albumRepository.Any(a=>a.AlbumId==idAlbum);
//        }
//    }
//}
