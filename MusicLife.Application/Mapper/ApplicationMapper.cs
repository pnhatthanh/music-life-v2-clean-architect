using AutoMapper;
using MusicLife.Application.Modules.M_Album.DTOs;
using MusicLife.Application.Modules.M_Artist.DTOs;
using MusicLife.Application.Modules.M_PlayList.DTOs;
using MusicLife.Application.Modules.M_Song.DTOs;
using MusicLife.Application.Modules.M_User.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicLife.Application.Mapper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Album, CreateAlbumDTO>().ReverseMap();
            CreateMap<Artist, CreateArtistDTO>().ReverseMap();
            CreateMap<PlayList, CreatePlayListDTO>().ReverseMap();
            CreateMap<UpdatePlayListDTO, PlayList>();
            CreateMap<CreateSongDTO, Song>();
            CreateMap<UpdateSongDTO, Song>();
            CreateMap<User, UserDTO>();

            CreateMap<Album, AlbumDTO>();
            CreateMap<PlayList, PlayListDTO>();
            CreateMap<Song, SongDTO>();
            CreateMap<Artist, ArtistDTO>();
            CreateMap<Album, SongDTO>();
        }
    }
}
