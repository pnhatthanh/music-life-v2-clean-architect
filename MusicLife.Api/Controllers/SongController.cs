using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLife.Api.Attributes;
using MusicLife.Application.Modules.M_Artist.Services;
using MusicLife.Application.Modules.M_Song.DTOs;
using MusicLife.Application.Modules.M_Song.Services;
using MusicLife.Domain.Entities;
using System.Security.Claims;

namespace MusicLife.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IArtistService _artistService;
        public SongController(ISongService songService, IArtistService artistService)
        {
            _songService = songService;  
            _artistService = artistService;
        }
        [HttpGet]
        [Cache(100)]
        public async Task<IActionResult> GetAllSongs([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var songs = await _songService.GetSongsAsync(page, pageSize);
            return Ok(songs.Item1);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong(Guid id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            return Ok(song);
        }
        [HttpPost("recently-play")]
        public Task<IActionResult> GetRecentLyPlay()
        {
           throw new NotImplementedException();
        }
        [HttpGet("search")]
        [Cache(100)]
        public async Task<IActionResult> SearchSongOrArtist([FromQuery(Name = "title")] string title)
        {
            var songs = await _songService.GetSongByNameAsync(title);
            var artists = await _artistService.GetArtistByNameAsync(title);
            return Ok(new { songs, artists });
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSong([FromForm] CreateSongDTO songDTO)
        {
            var song = await _songService.CreateSongAsync(songDTO);
            return Ok(song);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSong([FromRoute] Guid id)
        {
            await _songService.DeleteSongAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSong([FromRoute] Guid id, [FromForm] UpdateSongDTO songDTO)
        {
            var song = await _songService.UpdateSongAsync(id, songDTO);
            return Ok(song);
        }
        [HttpGet("favourites")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetFavourites([FromQuery] int page, [FromQuery] int pageSize)
        {
            var favourites = await _songService.GetFavouriteSongsAsync(page, pageSize);
            return Ok(favourites);
        }
        [HttpPut("favourite/add/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddSongToFavourite([FromRoute] Guid id)
        {
            var song = await _songService.AddSongToFavouritesAsync(id);
            return Ok(song);
        }
        [HttpPut("favourite/remove/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveSongFromFavourite([FromRoute] Guid id)
        {
            await _songService.RemoveSongFavouriteAsync(id);
            return Ok();
        }
    }
}
