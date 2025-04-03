using AutoMapper;
using MusicLife.Application.Dtos;
using MusicLife.Application.Modules.M_Album.DTOs;
using MusicLife.Application.Modules.M_Artist.DTOs;
using MusicLife.Application.Modules.M_PlayList.DTOs;
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
            CreateMap<Album, AlbumDTO>();
            CreateMap<Artist, CreateArtistDTO>().ReverseMap();
            CreateMap<PlayList, CreatePlayListDTO>().ReverseMap();
            CreateMap<Song, SongDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Song, SongResponse>();
            CreateMap<Artist, ArtistDTO>();
            CreateMap<Album, SongResponse>();
        }
    }
}
