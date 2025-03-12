using AutoMapper;
using MusicLife.Application.Dtos;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure.Mapper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Album, AlbumDTO>().ReverseMap();
            CreateMap<Artist, ArtistDTO>().ReverseMap();
            CreateMap<PlayList, PlayListDTO>().ReverseMap();
            CreateMap<Song, SongDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Song, SongResponse>();
            CreateMap<Artist, ArtistResponse>();
            CreateMap<Album, SongResponse>();
        }
    }
}
